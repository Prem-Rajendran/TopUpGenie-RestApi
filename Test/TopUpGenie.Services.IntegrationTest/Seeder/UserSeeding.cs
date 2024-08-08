namespace TopUpGenie.Services.IntegrationTest.Seeder;

public static class UserSeeding
{
	public static IEnumerable<User> GetUsers()
	{
        var users = new List<User>();

        for (int i = 0; i < 10; i++)
        {
            users.Add(new User
            {
                Id = i + 1,
                Name = $"User{i}",
                Password = "AQAAAAIAAYagAAAAEE7M6kLHn+cOGWwv5xFMCuJ0lb6+UbO27/EyLRZz/j6hO3rW2gETaBSq71fFHmuYzQ==", //1234
                PhoneNumber = $"123456{i}",
                Verified = i % 2 == 0,
                IsAdmin = false,
                Balance = 1000 + i * 10,
                CreatedAt = DateTime.UtcNow.AddDays(-i),
                UpdatedAt = DateTime.UtcNow
            });
        }

        return users;
    }
}

