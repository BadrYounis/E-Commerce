using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction.Contracts;
using System.Text;

namespace Presentation.Attributes;
internal class RedisCacheAttribute(int timeToLiveInSeconds = 120) : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var cacheService = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().CacheService;
        string key = GenerateKey(context.HttpContext.Request);
        var result = await cacheService.GetCachedValueAsync(key);
        if (result is not null)
        {
            context.Result = new ContentResult
            {
                Content = result,
                ContentType = "application/json1",
                StatusCode = StatusCodes.Status200OK
            };
            return;
        }
        var resultContext = await next.Invoke();
        if (resultContext.Result is OkObjectResult okObjResult)
        {
            await cacheService.SetCacheValueAsync(key, okObjResult, TimeSpan.FromSeconds(timeToLiveInSeconds));
        }
    }
    private string GenerateKey(HttpRequest request)
    {
        var key = new StringBuilder();
        key.Append(request.Path);       //api/Products
        foreach (var item in request.Query.OrderBy(x => x.Key))
        {
            key.Append($"{item.Key}-{item.Value}"); //api/Products/pageIndex-1&pageSize-10&sort-NameDesc
        }
        return key.ToString();
    }
}
