namespace TopUpGenie.Services.Models.RequestModels;

public class UpdateUserRequestModel : CreateUserRequestModel
{
	[Required]
	public int UserId { get; set; }

    public new string? Password { get; set; }

	public string ConfirmPassword { get; set; }

    public string OldPassword { get; set; }
}