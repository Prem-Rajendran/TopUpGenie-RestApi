namespace TopUpGenie.Services.IntegrationTest.Helper;

public class TransactionDbContextFixture
{
    public TransactionDbContext DbContext { get; }

    public TransactionDbContextFixture()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<TransactionDbContext>()
            .UseSqlite(connection)
            .Options;

        DbContext = new TransactionDbContext(options);
        DbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        DbContext.Database.EnsureDeleted();
        DbContext.Dispose();
    }
}

