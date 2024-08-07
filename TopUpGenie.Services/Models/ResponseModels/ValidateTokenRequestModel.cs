namespace TopUpGenie.Services.Models.ResponseModels;

/// <summary>
/// ValidateTokenRequestModel
/// </summary>
public class ValidateTokenRequestModel
{
	public int UserId;
	public TokenResponseModel? TokenResponse;
	public ClaimsPrincipal? claimsPrincipal;
}