using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared.Dtos.OrderModule;
using System.Security.Claims;

namespace Presentation.Controllers;

[Authorize]
public class OrdersController(IServiceManager _serviceManager) : ApiController
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderResult>> GetOrderByIdAsync(Guid id)
    {
        var order = await _serviceManager.OrderService.GetOrderByIdAsync(id);
        return Ok(order);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderResult>>> GetAllOrdersByEmail()
    {
        return Ok(await _serviceManager.OrderService.GetOrdersByEmailAsync(User.FindFirstValue(ClaimTypes.Email)!));
    }

    [HttpPost]
    public async Task<ActionResult<OrderResult>> CreateOrderAsync(OrderRequest orderRequest)
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        var order = await _serviceManager.OrderService.CreateOrderAsync(orderRequest, userEmail!);
        return Ok(order);
    }

    [HttpGet("DeliveryMethods")]
    public async Task<ActionResult<IEnumerable<DeliveryMethodResult>>> GetDeliveryMethodsAsync()
    {
        return Ok(await _serviceManager.OrderService.GetDeliveryMethodsAsync());
    }
}