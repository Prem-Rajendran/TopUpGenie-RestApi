namespace TopUpGenie.RestApi.Controllers.Api;

/// <summary>
/// AuthenticationController
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthenticationController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Login
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Login")]
    public async Task<IResponse<TokenResponseModel>> Login([FromBody] UserAuthenticationRequestModel model)
    {
        var context = HttpContext.GetRequestContext();
        var response = await _authService.AuthenticateAsync(context, model);
        return response.ToApiResponse(HttpContext);
    }

    /// <summary>
    /// Logout
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "admin, user")]
    [HttpDelete]
    [Route("Logout")]
    public async Task<IResponse<bool>> Logout()
    {
        var context = HttpContext.GetRequestContext();
        var response = await _authService.InvalidateTokenAsync(context);
        return response.ToApiResponse(HttpContext);
    }
}

