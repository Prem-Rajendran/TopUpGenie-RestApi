using System;
using TopUpGenie.Services.Helper;

namespace TopUpGenie.Services.Extensions
{
	public static class UserExtensions
	{
		public static void UpdatePassword(this User user, UpdateUserRequestModel requestModel, IResponse<bool> response)
		{
			if (user == null)
				return;

			if (string.IsNullOrWhiteSpace(requestModel.OldPassword))
			{
                response.Messages.Add(new Message { Description = "Password change not initiated" });
                return;
            }
				

			if (string.IsNullOrWhiteSpace(requestModel.Password)
				|| string.IsNullOrWhiteSpace(requestModel.ConfirmPassword)
				|| requestModel.Password != requestModel.ConfirmPassword)
			{
                response.Messages.Add(new Message { Description = "Password change not initiated. Password mismatch" });
                return;
            }

			
			if (!PasswordHelper.VerifyPassword(user.Password ?? "", requestModel.OldPassword))
			{
                response.Messages.Add(new Message { Description = "Password change not initiated. Incorrect old password" });
                return;
            }

			user.Password = PasswordHelper.GenerateHash(requestModel.Password);
		}
	}
}

