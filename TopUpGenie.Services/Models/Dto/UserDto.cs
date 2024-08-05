using System;
namespace TopUpGenie.Services.Models.Dto
{
	public class UserDto
	{
		public int UserId { get; set; }
		public string? UserName { get; set; }

		public UserDto(User user)
		{
			if (user != null)
			{
                UserId = user.Id;
                UserName = user.Name;
            }
		}
	}
}