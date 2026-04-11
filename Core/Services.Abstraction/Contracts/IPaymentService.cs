using Shared.Dtos.BasketModule;

namespace Services.Abstraction.Contracts;
public interface IPaymentService
{
    Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId);
    Task UpdatePaymentStatusAsync(string json, string signatureHeader);
}