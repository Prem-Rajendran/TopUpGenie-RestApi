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
}

