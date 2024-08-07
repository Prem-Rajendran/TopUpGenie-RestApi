namespace TopUpGenie.DataAccess.DataContext;

/// <summary>
/// DbInitializer
/// </summary>
public class DbInitializer
{
    /// <summary>
    /// Initialize
    /// </summary>
    /// <param name="context"></param>
    public static void Initialize(TopUpGenieDbContext context)
    {
        context.Database.EnsureCreated();

        if (!context.Users.Any())
        {
            // Seed Users
            context.Users.AddRange(
                new User
                {
                    Id = 1,
                    Name = "user1-admin",
                    PhoneNumber = "0000001",
                    Password = "AQAAAAIAAYagAAAAEN5sdVVIuZFjqtIAwcFiFa7DnlYgn89rujvz+KJE1sE097EJqlgpT5YiSHiR2hnGzw==",  //admin123
                    IsAdmin = true,
                    Balance = int.MaxValue,
                    Verified = true
                },
                new User
                {
                    Id = 2,
                    Name = "user2-verified",
                    PhoneNumber = "0000002",
                    Password = "AQAAAAIAAYagAAAAEE7M6kLHn+cOGWwv5xFMCuJ0lb6+UbO27/EyLRZz/j6hO3rW2gETaBSq71fFHmuYzQ==",  //1234
                    IsAdmin = false,
                    Balance = 4000,
                    Verified = true
                },
                new User
                {
                    Id = 3,
                    Name = "user3-verified",
                    PhoneNumber = "0000003",
                    Password = "AQAAAAIAAYagAAAAEE7M6kLHn+cOGWwv5xFMCuJ0lb6+UbO27/EyLRZz/j6hO3rW2gETaBSq71fFHmuYzQ==",  //1234
                    IsAdmin = false,
                    Balance = 2000,
                    Verified = true
                },
                new User
                {
                    Id = 4,
                    Name = "user4-verified",
                    PhoneNumber = "0000004",
                    Password = "AQAAAAIAAYagAAAAEE7M6kLHn+cOGWwv5xFMCuJ0lb6+UbO27/EyLRZz/j6hO3rW2gETaBSq71fFHmuYzQ==",  //1234
                    IsAdmin = false,
                    Balance = 500,
                    Verified = true
                },
                new User
                {
                    Id = 5,
                    Name = "user5-unverified",
                    PhoneNumber = "0000005",
                    Password = "AQAAAAIAAYagAAAAEE7M6kLHn+cOGWwv5xFMCuJ0lb6+UbO27/EyLRZz/j6hO3rW2gETaBSq71fFHmuYzQ==",  //1234
                    IsAdmin = false,
                    Balance = 3300,
                    Verified = false
                },
                new User
                {
                    Id = 6,
                    Name = "user6-unverified",
                    PhoneNumber = "0000006",
                    Password = "AQAAAAIAAYagAAAAEE7M6kLHn+cOGWwv5xFMCuJ0lb6+UbO27/EyLRZz/j6hO3rW2gETaBSq71fFHmuYzQ==",  //1234
                    IsAdmin = false,
                    Balance = 2500,
                    Verified = false
                },
                new User
                {
                    Id = 7,
                    Name = "user7-unverified",
                    PhoneNumber = "0000007",
                    Password = "AQAAAAIAAYagAAAAEE7M6kLHn+cOGWwv5xFMCuJ0lb6+UbO27/EyLRZz/j6hO3rW2gETaBSq71fFHmuYzQ==",  //1234
                    IsAdmin = false,
                    Balance = 1700,
                    Verified = false
                }
            );
        }

        if (!context.TopUpOptions.Any())
        {
            context.TopUpOptions.AddRange(
                new TopUpOption { TopUpOptionId = 1, Amount = 5, Description = "AED 5" },
                new TopUpOption { TopUpOptionId = 2, Amount = 10, Description = "AED 10" },
                new TopUpOption { TopUpOptionId = 3, Amount = 20, Description = "AED 20" },
                new TopUpOption { TopUpOptionId = 4, Amount = 30, Description = "AED 30" },
                new TopUpOption { TopUpOptionId = 5, Amount = 50, Description = "AED 50" },
                new TopUpOption { TopUpOptionId = 6, Amount = 75, Description = "AED 75" },
                new TopUpOption { TopUpOptionId = 7, Amount = 100, Description = "AED 100" }
            );
        }

        context.SaveChanges();
    }
}


