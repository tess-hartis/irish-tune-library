using System.Net;
using Newtonsoft.Json;

namespace TL.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _requestDelegate;
    public ExceptionHandlingMiddleware(RequestDelegate requestDelegate)
    {
        _requestDelegate = requestDelegate;
    }
    public async Task Invoke(HttpContext context)
    {

        try
        {
            await _requestDelegate(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }
    
    private static Task HandleException(HttpContext context, Exception ex)
    {
        var statusCode = (int) HttpStatusCode.InternalServerError;
        switch (ex)
        {
            case NullReferenceException:
                statusCode = (int) HttpStatusCode.InternalServerError;
                break;
            
        }
        
        var errorMessage = JsonConvert.SerializeObject(new {ex.Message});

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        return context.Response.WriteAsync(errorMessage);
    }
}