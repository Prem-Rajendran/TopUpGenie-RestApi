namespace TopUpGenie.Services.Models.RequestModels;

public class UserAuthenticationRequestModel
{
	public int UserId { get; set; }

    [StringLength(12, MinimumLength = 4, ErrorMessage = "Password should be within 4 character to 12 characters strictly")]
    public string? Password { get; set; }
}