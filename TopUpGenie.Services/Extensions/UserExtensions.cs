namespace TopUpGenie.Services.Extensions;

/// <summary>
/// UserExtensions
/// </summary>
public static class UserExtensions
{
    /// <summary>
    /// ToNewUser
    /// </summary>
    /// <param name="requestModel"></param>
    /// <returns></returns>
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

    /// <summary>
    /// UpdateUser
    /// </summary>
    /// <param name="user"></param>
    /// <param name="requestModel"></param>
    /// <param name="response"></param>
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
            response.AddMessage(ErrorCodes.ADMIN_PASSWORD_CHANGE_USER_NOT_FOUND, ErrorMessage.ADMIN_PASSWORD_CHANGE_USER_NOT_FOUND);
    }

    private static void UpdatePassword(this User user, UpdateUserRequestModel requestModel, IResponse<bool> response)
	{
		if (string.IsNullOrWhiteSpace(requestModel.OldPassword))
		{
            response.AddMessage(ErrorCodes.ADMIN_PASSWORD_CHANGE_NOT_INITIATED, ErrorMessage.ADMIN_PASSWORD_CHANGE_NOT_INITIATED);
            return;
        }
				

		if (string.IsNullOrWhiteSpace(requestModel.NewPassword)
			|| string.IsNullOrWhiteSpace(requestModel.ConfirmPassword)
			|| requestModel.NewPassword != requestModel.ConfirmPassword)
		{
            response.AddMessage(ErrorCodes.ADMIN_PASSWORD_CHANGE_MISMATCH, ErrorMessage.ADMIN_PASSWORD_CHANGE_MISMATCH);
            return;
        }

			
		if (!PasswordHelper.VerifyPassword(user.Password ?? "", requestModel.OldPassword))
		{
            response.AddMessage(ErrorCodes.ADMIN_PASSWORD_CHANGE_INCORRECT_OLD_PASSWORD, ErrorMessage.ADMIN_PASSWORD_CHANGE_INCORRECT_OLD_PASSWORD);
            return;
        }

		user.Password = PasswordHelper.GenerateHash(requestModel.NewPassword);
	}
}

