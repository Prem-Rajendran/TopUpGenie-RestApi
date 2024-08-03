using System;
using TopUpGenie_RestApi.Models;

namespace TopUpGenie_RestApi.Data;

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
            new Admin {
                Name = "Admin",
                Password = "admin123",
                IsAdmin = true
            }
        );

        context.SaveChanges();
    }
}


