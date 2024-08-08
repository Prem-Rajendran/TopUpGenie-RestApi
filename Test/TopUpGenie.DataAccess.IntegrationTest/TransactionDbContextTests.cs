namespace TopUpGenie.DataAccess.IntegrationTest;

public class TransactionDbContextTests
{
    private readonly TransactionDbContext _transactionDbContext;

    public TransactionDbContextTests()
    {
        _transactionDbContext = DependencyHelper.TransactionDbContext;
    }

    [Fact]
    public void DbContext_Configuration_IsCorrect()
    {
        Assert.NotNull(_transactionDbContext);
    }

    [Fact]
    public void DbContext_Schema_IsCorrect()
    {
        Assert.NotNull(_transactionDbContext.Users);
        Assert.NotNull(_transactionDbContext.Beneficiaries);
        Assert.NotNull(_transactionDbContext.TopUpOptions);
        Assert.NotNull(_transactionDbContext.Transactions);
    }

    [Fact]
    public void DbContext_CRUD_Operations_Work()
    {

        var user = new User { Name = "xxxx", Password = "1234", Balance = 10, IsAdmin = false, Verified = false, PhoneNumber = "1x34567" };
        _transactionDbContext.Users.Add(user);
        _transactionDbContext.SaveChanges();
        Assert.True(_transactionDbContext.Users.Any(u => u.PhoneNumber == "1x34567"));

        user.Balance = 1000;
        _transactionDbContext.Users.Update(user);
        _transactionDbContext.SaveChanges();
        Assert.True(_transactionDbContext.Users.Any(u => u.PhoneNumber == "1x34567" && u.Balance == 1000));

        _transactionDbContext.Users.Remove(user);
        _transactionDbContext.SaveChanges();

        var entity = _transactionDbContext.Users.Find(user.Id);
        Assert.Null(entity);
    }
}

