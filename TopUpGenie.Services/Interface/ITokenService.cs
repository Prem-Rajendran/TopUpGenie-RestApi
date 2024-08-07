namespace TopUpGenie.Services.Interface;

/// <summary>
/// ITokenService
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// GenerateToken
    /// </summary>
    /// <param name="response"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<TokenResponseModel?> GenerateToken(IResponse<TokenResponseModel> response, User user);

    /// <summary>
    /// InvalidateToken
    /// </summary>
    /// <param name="response"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<bool> InvalidateToken(IResponse<bool> response, User user);

    /// <summary>
    /// ValidateToken
    /// </summary>
    /// <param name="response"></param>
    /// <param name="accessToken"></param>
    /// <returns></returns>
    Task<IResponse<ValidateTokenRequestModel>?> ValidateToken(IResponse<ValidateTokenRequestModel> response, string accessToken);
}