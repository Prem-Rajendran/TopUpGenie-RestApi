namespace TopUpGenie.DataAccess.DataContext;

public class TopUpGenieDbContext : DbContext
{
    public TopUpGenieDbContext(DbContextOptions<TopUpGenieDbContext> options) : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<LoginSession> LoginSessions { get; set; }
    public DbSet<Beneficiary> Beneficiaries { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureAccountTable(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("DataSource=Database/TopUpGenieDb.db",
                b => b.MigrationsAssembly("TopUpGenie.RestApi"));
        }
    }

    private static void ConfigureAccountTable(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>()
           .Property(a => a.Id)
           .ValueGeneratedOnAdd(); // Auto-increment

        // Configure a unique constraint on the AccountNumber property
        modelBuilder.Entity<Account>()
            .HasIndex(a => a.AccountNumber)
            .IsUnique(); // Ensure AccountNumber is unique
    }
}