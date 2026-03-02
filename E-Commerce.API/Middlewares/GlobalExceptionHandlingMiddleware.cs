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
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something Went Wrong: {ex.Message}");
            await HandleExceptionAsync(context, ex);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        //1) Change StatusCode 
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
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