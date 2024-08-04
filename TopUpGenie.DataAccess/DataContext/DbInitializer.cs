namespace TopUpGenie.DataAccess.DataContext;

public class DbInitializer
{
    public static void Initialize(TopUpGenieDbContext context)
    {
        context.Database.EnsureCreated();

        // Check if data already exists
        if (context.Admins.Any())
            return; // DB has been seeded

        // Seed Users
        context.Admins.AddRange(
            new Admin
            {
                Name = "Admin",
                Password = "AQAAAAIAAYagAAAAEJlUmFFlySnamcy/Hlvwnl4OAItFswCOjs0nZupeE5fBwfLHinLz5JcKDVB8u3K+qA==",  //"admin123"
                IsAdmin = true
            }
        );

        context.SaveChanges();
    }
}


