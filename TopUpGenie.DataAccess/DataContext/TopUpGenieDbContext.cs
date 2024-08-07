namespace TopUpGenie.DataAccess.DataContext;

/// <summary>
/// TopUpGenieDbContext
/// </summary>
[ExcludeFromCodeCoverage]
public class TopUpGenieDbContext : DbContext
{
    public TopUpGenieDbContext(DbContextOptions<TopUpGenieDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<LoginSession> LoginSessions { get; set; }
    public DbSet<Beneficiary> Beneficiaries { get; set; }
    public DbSet<TopUpOption> TopUpOptions { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureAccountTable(modelBuilder);
        ConfigureBeneficiaryTable(modelBuilder);
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

    private static void ConfigureBeneficiaryTable(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Beneficiary>()
            .HasOne(b => b.BeneficiaryUser)
            .WithMany()
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Beneficiary>()
            .HasOne(b => b.CreatedByUser)
            .WithMany()
            .HasForeignKey(b => b.CreatedByUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}