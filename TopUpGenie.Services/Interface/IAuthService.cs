namespace TopUpGenie.Services.Interface;

public interface IAuthService
{
    Task<IResponse<TokenResponseModel>> AuthenticateAsync(RequestContext requestContext, UserAuthenticationRequestModel requestModel);
}