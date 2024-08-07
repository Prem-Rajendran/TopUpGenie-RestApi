namespace TopUpGenie.DataAccess.DataContext;

/// <summary>
/// TransactionDbContext
/// </summary>
public class TransactionDbContext : DbContext
{
    public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Beneficiary> Beneficiaries { get; set; }
    public DbSet<TopUpOption> TopUpOptions { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("DataSource=Database/TopUpGenieDb.db",
                b => b.MigrationsAssembly("TopUpGenie.RestApi"));
        }
    }
}