using System;
namespace TopUpGenie.Services.Interface
{
	public interface ITokenService
	{
        Task<TokenResponseModel?> GenerateToken(IResponse<TokenResponseModel> response, User user, bool isAdmin);
    }
}