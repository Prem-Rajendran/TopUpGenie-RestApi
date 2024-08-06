using System;
namespace TopUpGenie.DataAccess.DataContext
{
	public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
        {
        }

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
}

