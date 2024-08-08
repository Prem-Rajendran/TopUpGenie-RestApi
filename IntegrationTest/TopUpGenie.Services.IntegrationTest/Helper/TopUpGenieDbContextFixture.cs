namespace TopUpGenie.Services.IntegrationTest.Helper;

public class TopUpGenieDbContextFixture : IDisposable
{
    public TopUpGenieDbContext DbContext { get; }

    public TopUpGenieDbContextFixture()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<TopUpGenieDbContext>()
            .UseSqlite(connection)
            .Options;

        DbContext = new TopUpGenieDbContext(options);
        DbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        DbContext.Database.EnsureDeleted();
        DbContext.Dispose();
    }
}