namespace TopUpGenie.RestApi.Middleware;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

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

// Extension method used to add the middleware to the HTTP request pipeline.
public static class AuthenticationMiddlewareExtensions
{
    public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthenticationMiddleware>();
    }
}

