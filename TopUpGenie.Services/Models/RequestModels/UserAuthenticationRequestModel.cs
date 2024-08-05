namespace TopUpGenie.Services.Models.RequestModels;

public class UserAuthenticationRequestModel
{
	public int UserId { get; set; }
	public string? Password { get; set; }
}