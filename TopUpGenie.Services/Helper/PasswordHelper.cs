using Microsoft.AspNetCore.Identity;
using TopUpGenie.DataAccess.DataModel;

namespace TopUpGenie.Services.Helper
{
	public static class PasswordHelper
	{
		public static string GenerateHash(User user)
		{
            var hasher = new PasswordHasher<User>();
			return hasher.HashPassword(user, user.Password);
        }
	}
}