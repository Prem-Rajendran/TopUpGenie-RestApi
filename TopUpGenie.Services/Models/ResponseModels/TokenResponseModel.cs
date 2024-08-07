namespace TopUpGenie.Services.Models.ResponseModels;

/// <summary>
/// TokenResponseModel
/// </summary>
public class TokenResponseModel
{
	public string? AccessToken { get; set; }
	public string? RefreshToken { get; set; }
	public DateTime Expiration { get; set; }
}