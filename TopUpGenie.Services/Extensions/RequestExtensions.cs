namespace TopUpGenie.Services.Extensions;

public static class RequestExtensions
{
	public static User? ToUser(this UpdateUserRequestModel requestModel, User user)
	{
            if (requestModel != null && user != null)
            {
			string confirmPassword = PasswordHelper.GenerateHash(requestModel.ConfirmPassword);
			
                user.Name = requestModel.Name;
			if (requestModel.OldPassword != null)
				user.Password = confirmPassword;
                user.Verified = requestModel.IsVerified;
            }

            return user;
        }

	public static User ToUser(this CreateUserRequestModel requestModel)
	{
		User user = new();

		if (requestModel != null)
		{
			user.Name = requestModel.Name;
			user.Password = PasswordHelper.GenerateHash(requestModel.Password ?? "");
			user.Verified = requestModel.IsVerified;
		}

		return user;
	}

	public static Account ToAccount(this CreateUserRequestModel requestModel)
	{
		Account account = new();

		if (requestModel != null)
		{
			account.AccountNumber = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
                account.Currency = string.IsNullOrWhiteSpace(requestModel.Currency) ? "AED" : requestModel.Currency;
			account.Balance = requestModel.InitialBalance;
        }

		return account;
	}
}

