namespace TopUpGenie.RestApi.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }

    [Authorize(Roles = "admin, user")]
    [HttpGet]
    [Route("")]
    public async Task<IResponse<ProfileResponseModel>> Get()
    {
        var context = HttpContext.GetRequestContext();
        return await _profileService.GetMyProfile(context);
    }
}