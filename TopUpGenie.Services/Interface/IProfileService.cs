namespace TopUpGenie.Services.Interface;

/// <summary>
/// IProfileService
/// </summary>
public interface IProfileService
{
    /// <summary>
    /// GetMyProfile
    /// </summary>
    /// <param name="requestContext"></param>
    /// <returns></returns>
    Task<IResponse<ProfileResponseModel>> GetMyProfile(RequestContext requestContext);
}

