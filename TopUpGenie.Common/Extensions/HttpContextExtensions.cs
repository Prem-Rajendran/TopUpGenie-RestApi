namespace TopUpGenie.Common.Extensions;

/// <summary>
/// HttpContextExtensions
/// </summary>
[ExcludeFromCodeCoverage]
public static class HttpContextExtensions
{
    /// <summary>
    /// GetRequestContext
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static RequestContext GetRequestContext(this HttpContext httpContext)
    {
        // Ensure RequestContext is registered and can be retrieved
        RequestContext? requestContext = httpContext.RequestServices.GetService(typeof(RequestContext)) as RequestContext ?? throw new InvalidOperationException("RequestContext is not registered.");
        return requestContext;
    }
}