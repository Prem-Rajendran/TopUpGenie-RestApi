namespace TopUpGenie.Services.IntegrationTest.Seeder;

public static class LoginSessionSeeding
{
	public static IEnumerable<LoginSession> GetLoginSessions()
	{
        List<LoginSession> loginSessions = new List<LoginSession>
        {
            new LoginSession
            {
                UserId = 1,
                AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwidW5pcXVlX25hbWUiOiJVc2VyMCIsInJvbGUiOiJ1c2VyIiwibmJmIjoxNzIzMDg4NjU5LCJleHAiOjE3MjMwODg3MTksImlhdCI6MTcyMzA4ODY1OSwiaXNzIjoiVG9wVXBHZW5pZUlzc3VlciIsImF1ZCI6IlRvcFVwR2VuaWVBdWRpZW5jZSJ9.WOdmusruoB22IKK-YmZ6VT4tF99tYRhagsRITLFP_mI",
                RefreshToken = "RefreshToken",
                CreatedAt = DateTime.Now.AddMinutes(-60),
                ExpirationDateTime = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new LoginSession
            {
                UserId = 2,
                AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwidW5pcXVlX25hbWUiOiJVc2VyMCIsInJvbGUiOiJ1c2VyIiwibmJmIjoxNzIzMDg4NjU5LCJleHAiOjE3MjMwODg3MTksImlhdCI6MTcyMzA4ODY1OSwiaXNzIjoiVG9wVXBHZW5pZUlzc3VlciIsImF1ZCI6IlRvcFVwR2VuaWVBdWRpZW5jZSJ9.WOdmusruoB22IKK-YmZ6VT4tF99tYRhagsRITLFP_mI",
                RefreshToken = "RefreshToken",
                CreatedAt = DateTime.Now,
                ExpirationDateTime = DateTime.Now.AddMinutes(5),
                UpdatedAt = DateTime.Now
            }
        };

        return loginSessions;
    }
}

