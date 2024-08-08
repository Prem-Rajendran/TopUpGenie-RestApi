namespace TopUpGenie.DataAccess.IntegrationTest.Helper;

public static class LoginSessionSeeding
{
	public static IEnumerable<LoginSession> GetLoginSessions()
	{
		List<LoginSession> loginSessions = new List<LoginSession>();

            for (int i = 0; i < 5; i++)
		{
			loginSessions.Add(new LoginSession
			{
				UserId = i + 1,
				AccessToken = "AccessToken",
				RefreshToken = "RefreshToken",
				CreatedAt = DateTime.Now,
				ExpirationDateTime = DateTime.Now.AddHours(2),
				UpdatedAt = DateTime.Now
			});

            }

		return loginSessions;
    }
}

