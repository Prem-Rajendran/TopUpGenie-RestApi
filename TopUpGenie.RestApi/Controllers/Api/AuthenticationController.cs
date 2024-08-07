namespace TopUpGenie.RestApi.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly TopUpGenieDbContext _genieDbContext;
    private readonly TransactionDbContext _transactionDbContext;

    public AuthenticationController(IAuthService authService, TopUpGenieDbContext genieDbContext, TransactionDbContext transactionDbContext)
    {
        _authService = authService;
        _genieDbContext = genieDbContext;
        _transactionDbContext = transactionDbContext;
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

