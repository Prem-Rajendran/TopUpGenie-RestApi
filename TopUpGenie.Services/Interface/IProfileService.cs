namespace TopUpGenie.Services.Interface;

public interface IProfileService
{
    Task<IResponse<ProfileResponseModel>> GetMyProfile(RequestContext requestContext);
}

