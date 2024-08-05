namespace TopUpGenie.RestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private IAuthService _authService;

    public AuthenticationController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IResponse<TokenResponseModel>> Login([FromBody] UserAuthenticationRequestModel model)
    {
        var context = HttpContext.GetRequestContext();
        return await _authService.AuthenticateAsync(context, model);
    }

    [Authorize(Roles = "admin, user")]
    [HttpDelete]
    [Route("Logout")]
    public async Task<IResponse<bool>> Logout()
    {
        var context = HttpContext.GetRequestContext();
        return await _authService.InvalidateTokenAsync(context);
    }
}

