using System;
namespace TopUpGenie.Services.Interface
{
	public interface ITokenService
	{
        Task<TokenResponseModel?> GenerateToken(IResponse<TokenResponseModel> response, User user, bool isAdmin);

        Task<bool> InvalidateToken(IResponse<bool> response, User user);

        Task<IResponse<ValidateTokenRequestModel>?> ValidateToken(IResponse<ValidateTokenRequestModel> response, string accessToken);
    }
}