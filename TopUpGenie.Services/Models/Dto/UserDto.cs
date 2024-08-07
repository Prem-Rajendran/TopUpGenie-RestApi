namespace TopUpGenie.Services.Models.Dto;

/// <summary>
/// UserDto
/// </summary>
public class UserDto
{
	public int UserId { get; set; }
	public string? UserName { get; set; }
	public bool Verified { get; set; }
	public int Balance { get; set; }
	public string? PhoneNumber { get; set; }

	public UserDto(User user)
	{
		if (user != null)
		{
            UserId = user.Id;
            UserName = user.Name;
			Verified = user.Verified;
			Balance = user.Balance;
			PhoneNumber = user.PhoneNumber;
        }
	}
}