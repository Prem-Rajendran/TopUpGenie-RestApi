using System;
namespace TopUpGenie.Services.Models.ResponseModels
{
	public class ValidateTokenRequestModel
	{
		public int UserId;
		public TokenResponseModel? TokenResponse;
		public ClaimsPrincipal? claimsPrincipal;
	}
}

