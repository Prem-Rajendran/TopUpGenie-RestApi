namespace TopUpGenie.Services.Interface;

/// <summary>
/// IAuthService
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// AuthenticateAsync
    /// </summary>
    /// <param name="requestContext"></param>
    /// <param name="requestModel"></param>
    /// <returns></returns>
    Task<IResponse<TokenResponseModel>> AuthenticateAsync(RequestContext requestContext, UserAuthenticationRequestModel requestModel);

    /// <summary>
    /// InvalidateTokenAsync
    /// </summary>
    /// <param name="requestContext"></param>
    /// <returns></returns>
    Task<IResponse<bool>> InvalidateTokenAsync(RequestContext requestContext);
}