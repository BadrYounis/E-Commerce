using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared.Dtos.BasketModule;

namespace Presentation.Controllers;
public class BasketController(IServiceManager _serviceManager) : ApiController
{
    //Get  BaseUrl/api/Basket?id=Basket01
    [HttpGet]
    public async Task<ActionResult<BasketDto>> GetBasketAsync(string id)
        => Ok(await _serviceManager.BasketService.GetBasketAsync(id));

    //Post
    [HttpPost]
    public async Task<ActionResult<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basket)
        => Ok(await _serviceManager.BasketService.CreateOrUpdateBasketAsync(basket));

    //Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBasketAsync(string id)
    {
        await _serviceManager.BasketService.DeleteBasketAsync(id);
        return NoContent();
    }
}