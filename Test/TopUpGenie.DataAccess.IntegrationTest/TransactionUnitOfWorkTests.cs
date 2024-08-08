

namespace TopUpGenie.DataAccess.IntegrationTest;

[Collection("Class")]
public class TransactionUnitOfWorkTests
{
    private readonly ITransactionUnitOfWork unitOfWork;
    private readonly TransactionDbContext dbContext;

	public TransactionUnitOfWorkTests()
	{
        dbContext = DependencyHelper.TransactionDbContext;
        unitOfWork = new TransactionUnitOfWork(dbContext, new LoggerFactory());
    }

    [Fact]
    public async Task TransactionRepository_NotNull()
    {
        var users = await unitOfWork.Transactions.GetLastFiveTransactions();
        Assert.NotNull(users);
        Assert.True(users.Any());
    }

    [Fact]
    public async Task CompleteAsync_SavesChangesToDatabase()
    {
        var userRepository = unitOfWork.Transactions;

        var transaction = DependencyHelper.ValidTransaction;
        await userRepository.AddAsync(transaction);
        await unitOfWork.CompleteAsync();

        Assert.True(dbContext.Users.Any(u => u.Id != 0));
    }
}