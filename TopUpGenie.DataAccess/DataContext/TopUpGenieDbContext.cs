namespace TopUpGenie.DataAccess.DataContext;

public class TopUpGenieDbContext : DbContext
{
    public TopUpGenieDbContext(DbContextOptions<TopUpGenieDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<LoginSession> LoginSessions { get; set; }
    public DbSet<Beneficiary> Beneficiaries { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TopUpOption> TopUpOptions { get; set; }

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
        modelBuilder.Entity<User>()
            .HasIndex(a => a.PhoneNumber)
            .IsUnique();

        modelBuilder.Entity<TopUpOption>()
            .HasIndex(a => a.Amount)
            .IsUnique();
    }
}