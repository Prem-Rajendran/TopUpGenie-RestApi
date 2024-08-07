namespace TopUpGenie.RestApi.Controllers.Api;

/// <summary>
/// ProfileController
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }

    /// <summary>
    /// Get
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "admin, user")]
    [HttpGet]
    [Route("")]
    public async Task<IResponse<ProfileResponseModel>> Get()
    {
        var context = HttpContext.GetRequestContext();
        var response = await _profileService.GetMyProfile(context);
        return response.ToApiResponse(HttpContext);
    }
}