namespace TopUpGenie.DataAccess.IntegrationTest.Helper;

public static class UserSeeding
{
	public static IEnumerable<User> GetUsers()
	{
        var users = new List<User>();

        for (int i = 0; i < 10; i++)
        {
            users.Add(new User
            {
                Name = $"User{i}",
                Password = $"Password{i}",
                PhoneNumber = $"123456{i}",
                Verified = i % 2 == 0,
                IsAdmin = i % 4 == 0,
                Balance = 1000 + i * 10,
                CreatedAt = DateTime.UtcNow.AddDays(-i),
                UpdatedAt = DateTime.UtcNow
            });
        }

        return users;
    }
}

