using System;
using TopUpGenie.Services.Helper;

namespace TopUpGenie.Services.Extensions
{
	public static class UserExtensions
	{
        public static User? ToNewUser(this CreateUserRequestModel requestModel)
        {
            User? user = null;

            if (requestModel != null)
            {
                user = new User
                {
                    Name = requestModel.Name,
                    Password = PasswordHelper.GenerateHash(requestModel.Password ?? ""),
                    Verified = requestModel.IsVerified,
                    Balance = requestModel.InitialBalance,
                    PhoneNumber = requestModel.PhoneNumber,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
            }

            return user;
        }

        public static void UpdateUser(this User user, UpdateUserRequestModel requestModel, IResponse<bool> response)
        {
            if (requestModel != null && user != null)
            {
                user.Name = !string.IsNullOrWhiteSpace(requestModel.Name) ? requestModel.Name : user.Name;
                user.PhoneNumber = !string.IsNullOrWhiteSpace(requestModel.PhoneNumber) ? requestModel.PhoneNumber : user.PhoneNumber;
                user.Verified = requestModel.IsVerified != null ? requestModel.IsVerified.Value : user.Verified;
                user.Balance += requestModel.Money;
                user.UpdatePassword(requestModel, response);
            }
            else
                response.Messages.Add(new Message { Description = "Password change not initiated. User not found" });
        }

        private static void UpdatePassword(this User user, UpdateUserRequestModel requestModel, IResponse<bool> response)
		{
			if (string.IsNullOrWhiteSpace(requestModel.OldPassword))
			{
                response.Messages.Add(new Message { Description = "Password change not initiated" });
                return;
            }
				

			if (string.IsNullOrWhiteSpace(requestModel.NewPassword)
				|| string.IsNullOrWhiteSpace(requestModel.ConfirmPassword)
				|| requestModel.NewPassword != requestModel.ConfirmPassword)
			{
                response.Messages.Add(new Message { Description = "Password change not initiated. Password mismatch" });
                return;
            }

			
			if (!PasswordHelper.VerifyPassword(user.Password ?? "", requestModel.OldPassword))
			{
                response.Messages.Add(new Message { Description = "Password change not initiated. Incorrect old password" });
                return;
            }

			user.Password = PasswordHelper.GenerateHash(requestModel.NewPassword);
		}
	}
}

