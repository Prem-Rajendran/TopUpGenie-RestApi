namespace TopUpGenie.RestApi.Middleware;

/// <summary>
/// AuthenticationMiddleware
/// </summary>
public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Invoke
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="tokenService"></param>
    /// <returns></returns>
    public async Task Invoke(HttpContext httpContext, ITokenService tokenService)
    {
        var accessToken = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        IResponse<ValidateTokenRequestModel> response = new GenericServiceResponse<ValidateTokenRequestModel> { Status = Common.Enums.Status.Unknown };
        if (!string.IsNullOrWhiteSpace(accessToken) && accessToken.Length > 10)
        {
            tokenService?.ValidateToken(response, accessToken);

            if (response.Status != Common.Enums.Status.Success)
            {
                httpContext.Response.StatusCode = 401;
                await httpContext.Response.WriteAsJsonAsync(response);
                return;
            }

            httpContext.Request.Headers["Authorization"] = $"Bearer {response?.Data?.TokenResponse?.AccessToken}";
            httpContext.Request.Headers["X-Access-Token"] = response?.Data?.TokenResponse?.AccessToken;
            httpContext.Request.Headers["X-Refresh-Token"] = response?.Data?.TokenResponse?.RefreshToken;
            httpContext.Request.Headers["X-User-ID"] = response?.Data?.UserId.ToString();
        }

        await _next(httpContext);
    }
}

/// <summary>
/// AuthenticationMiddlewareExtensions
/// </summary>
public static class AuthenticationMiddlewareExtensions
{
    public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthenticationMiddleware>();
    }
}

