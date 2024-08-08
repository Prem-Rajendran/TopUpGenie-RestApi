namespace TopUpGenie.DataAccess.IntegrationTest.RepositoryTests;

public class TransactionRepositoryTests
{
    private readonly TransactionRepository repository;
    private readonly TransactionDbContext dbContext;

    public TransactionRepositoryTests()
    {
        dbContext = DependencyHelper.TransactionDbContext;
        repository = new TransactionRepository(dbContext, new NullLogger<Repository<Transaction>>());
    }

    [Fact]
    public async Task TransactionRepository_Add_ValidTransaction()
    {
        var dbContext = DependencyHelper.TransactionDbContext;
        var repository = new TransactionRepository(dbContext, new NullLogger<Repository<Transaction>>());

        var transaction = DependencyHelper.ValidTransaction;
        bool status = await repository.AddAsync(transaction);
        await dbContext.SaveChangesAsync();
        Assert.True(status);
    }

    [Fact]
    public async Task TransactionRepository_Add_InValidTransaction()
    {
        var transaction = new Transaction();
        bool status = await repository.AddAsync(transaction);
        Assert.Throws<DbUpdateException>(() => dbContext.SaveChanges());
    }

    [Fact]
    public async Task TransactionRepository_Update_ValidTransaction()
    {
        var transaction = await dbContext.Transactions.FindAsync(1);
        transaction.Messages = "Updated Message";
        bool status = repository.Update(transaction);
        await dbContext.SaveChangesAsync();

        Assert.True(status);
    }

    [Fact]
    public async Task TransactionRepository_Update_InValidTransaction()
    {
        var transaction = await dbContext.Transactions.FindAsync(1);
        bool status = repository.Update(new Transaction());
        Assert.Throws<DbUpdateException>(() => dbContext.SaveChanges());
    }

    [Fact]
    public async Task TransactionRepository_GetLastFiveTransactions_Success()
    {
        var transactions = await repository.GetLastFiveTransactions();

        Assert.NotNull(transactions);
        Assert.True(transactions.Any());
    }

    [Fact]
    public async Task TransactionRepository_GetTotalMonthlySpends_Success()
    {
        int spends = await repository.GetTotalMonthlySpends(1);
        Assert.True(spends > 0);
    }

    [Fact]
    public async Task TransactionRepository_GetTotalMonthlySpends_Failure()
    {
        int spends = await repository.GetTotalMonthlySpends(100);
        Assert.True(spends == 0);
    }

    [Fact]
    public async Task TransactionRepository_GetTotalMonthlySpendsPerBeneficiary_Success()
    {
        int spends = await repository.GetTotalMonthlySpendsPerBeneficiary(1, 2);
        Assert.True(spends > 0);
    }

    [Fact]
    public async Task TransactionRepository_GetTotalMonthlySpendsPerBeneficiary_Failure()
    {
        int spends = await repository.GetTotalMonthlySpendsPerBeneficiary(100, 200);
        Assert.True(spends == 0);
    }

    [Fact]
    public async Task TransactionRepository_GetUsersMonthlyTransactions_Success()
    {
        var transactions = await repository.GetUsersMonthlyTransactions(1);
        Assert.NotNull(transactions);
        Assert.True(transactions.Any());
    }

    [Fact]
    public async Task TransactionRepository_GetUsersMonthlyTransactions_Failure()
    {
        var transactions = await repository.GetUsersMonthlyTransactions(100);
        Assert.False(transactions.Any());
    }
}

