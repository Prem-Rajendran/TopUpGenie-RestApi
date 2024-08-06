namespace TopUpGenie.RestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfileController : ControllerBase
{
    private readonly IAdminService _adminService;

    public ProfileController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [Authorize(Roles = "admin, user")]
    [HttpGet]
    [Route("")]
    public async Task<IResponse<UserDto>> Get()
    {
        var context = HttpContext.GetRequestContext();
        return await _adminService.GetUserByIdAsync(context, context.UserId);
    }
}

