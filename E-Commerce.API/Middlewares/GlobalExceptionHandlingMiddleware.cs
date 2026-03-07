using Domain.Exceptions;
using Shared.ErrorModels;

namespace E_Commerce.API.Middlewares;
public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
    public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            //404 Product NotFound: Throw Exception
            //500 Internal Server Error: Throw Exception By Default (The only case to complete inside try, there is problem and doesn't throw exception)
            await _next(context);
            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                await HandleNotFoundApiAsync(context);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something Went Wrong: {ex.Message}");
            await HandleExceptionAsync(context, ex);
        }
    }
    private async Task HandleNotFoundApiAsync(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        var response = new ErrorDetails
        {
            StatusCode = StatusCodes.Status404NotFound,
            ErrorMessage = $"The EndPoint With URL {context.Request.Path} not Found"
        }.ToString();
        await context.Response.WriteAsync(response);
    }
    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        //1) Change StatusCode 
        //context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.StatusCode = ex switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            (_) => StatusCodes.Status500InternalServerError
        };
        //2) Change Content Type
        context.Response.ContentType = "application/json";
        //3) Write Response Body
        var response = new ErrorDetails
        {
            StatusCode = context.Response.StatusCode,
            ErrorMessage = ex.Message
        }.ToString();
        await context.Response.WriteAsync(response);
    }
}