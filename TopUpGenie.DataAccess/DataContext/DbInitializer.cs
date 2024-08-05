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
                Password = "AQAAAAIAAYagAAAAEN5sdVVIuZFjqtIAwcFiFa7DnlYgn89rujvz+KJE1sE097EJqlgpT5YiSHiR2hnGzw==",  //"admin123"
                IsAdmin = true
            }
        );

        context.SaveChanges();
    }
}


