using AutoMapper;
using Domain.Contracts;
using Domain.Entities.BasketModule;
using Domain.Entities.OrderModule;
using Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Services.Abstraction.Contracts;
using Shared.Dtos.BasketModule;
using Stripe;
using Product = Domain.Entities.ProductModule.Product;

namespace Services.Implementations;
public class PaymentService(IConfiguration _configuration,
    IBasketRepository _basketRepository,
    IUnitOfWork _unitOfWork,
    IMapper _mapper) : IPaymentService
{
    public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
    {
        StripeConfiguration.ApiKey = _configuration.GetSection("StripeSettings")["SecretKey"];

        var basket = await GetBasketAsync(basketId);
        await ValidateBasketAsync(basket);

        var amount = CalculateTotalAsync(basket);
        await CreationOrUpdatePaymentIntentAsync(basket, amount);

        await _basketRepository.CreateOrUpdateBasketAsync(basket);
        return _mapper.Map<BasketDto>(basket);
    }
    private async Task<CustomerBasket> GetBasketAsync(string basketId)
    {
        return await _basketRepository.GetBasketAsync(basketId)
            ?? throw new BasketNotFoundException(basketId);
    }
    private async Task ValidateBasketAsync(CustomerBasket basket)
    {

        //3] Validate items price ==> [basket.item.price = product.price] == > product from db
        foreach (var item in basket.Items)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id)
                ?? throw new ProductNotFoundException(item.Id);
            item.Price = product.Price;
        }

        //4] Validate shipping price ==> get deliveryMethod [DeliveryMethodId] ==> shippingPrice = DeliveryMethod.Price
        if (!basket.DeliveryMethodId.HasValue) throw new Exception("No Delivery Method Selected");
        var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>()
            .GetByIdAsync(basket.DeliveryMethodId.Value)
            ?? throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId.Value);
        basket.ShippingPrice = deliveryMethod.Price;
    }
    private long CalculateTotalAsync(CustomerBasket basket)
    {
        //5] Total ==> [SubTotal + ShippingPrice] ==> cent ==> * 100 ==> Long
        //         ==> (long)([basket.items.q * basket.items.price] + shippingPrice[DeliveryMethod.Price]) * 100
        return (long)(basket.Items.Sum(i => i.Quantity * i.Price) + basket.ShippingPrice!) * 100;
    }
    private async Task CreationOrUpdatePaymentIntentAsync(CustomerBasket basket, long amount)
    {
        //6] Create or update paymentIntentId
        var stripeService = new PaymentIntentService();
        if (string.IsNullOrEmpty(basket.PaymentIntentId))
        {
            //Create
            var options = new PaymentIntentCreateOptions()
            {
                Amount = amount,       //Total ==> [SubTotal + ShippingPrice]
                Currency = "USD",
                PaymentMethodTypes = ["card"]
            };
            var paymentIntent = await stripeService.CreateAsync(options);
            basket.PaymentIntentId = paymentIntent.Id;
            basket.ClientSecret = paymentIntent.ClientSecret;
        }
        else
        {
            //Update
            //1) Product In DB => Price Changed
            //2) User + - Quantity
            //3) User Change Delivery Method
            //4) Admin change delivery method price
            var options = new PaymentIntentUpdateOptions()
            {
                Amount = amount
            };
            await stripeService.UpdateAsync(basket.PaymentIntentId, options);
        }
    }
}