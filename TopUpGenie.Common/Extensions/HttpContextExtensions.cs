namespace TopUpGenie.Common.Extensions;

public static class HttpContextExtensions
{
    public static RequestContext GetRequestContext(this HttpContext httpContext)
    {
        // Ensure RequestContext is registered and can be retrieved
        RequestContext? requestContext = httpContext.RequestServices.GetService(typeof(RequestContext)) as RequestContext ?? throw new InvalidOperationException("RequestContext is not registered.");
        return requestContext;
    }
}