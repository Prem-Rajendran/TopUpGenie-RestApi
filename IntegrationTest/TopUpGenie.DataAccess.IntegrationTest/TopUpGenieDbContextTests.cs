using TopUpGenie.DataAccess.IntegrationTest.Helper;

namespace TopUpGenie.DataAccess.IntegrationTest;

public class TopUpGenieDbContextTests
{
    private readonly TopUpGenieDbContext _topUpGenieDbContext;

    public TopUpGenieDbContextTests()
    {
        _topUpGenieDbContext = DependencyHelper.TopUpGenieDbContext;
    }

    [Fact]
    public void DbContext_Configuration_IsCorrect()
    {
        Assert.NotNull(_topUpGenieDbContext);
    }

    [Fact]
    public void DbContext_Schema_IsCorrect()
    {
        Assert.NotNull(_topUpGenieDbContext.Users);
        Assert.NotNull(_topUpGenieDbContext.LoginSessions);
        Assert.NotNull(_topUpGenieDbContext.Beneficiaries);
        Assert.NotNull(_topUpGenieDbContext.TopUpOptions);
        Assert.NotNull(_topUpGenieDbContext.Transactions);
    }

    [Fact]
    public void DbContext_CRUD_Operations_Work()
    {
        var user = new User { Name = "xxxx", Password = "1234", Balance = 10, IsAdmin = false, Verified = false, PhoneNumber = "1x34567" };
        _topUpGenieDbContext.Users.Add(user);
        _topUpGenieDbContext.SaveChanges();
        Assert.True(_topUpGenieDbContext.Users.Any(u => u.PhoneNumber == "1x34567"));

        user.Balance = 1000;
        _topUpGenieDbContext.Users.Update(user);
        _topUpGenieDbContext.SaveChanges();
        Assert.True(_topUpGenieDbContext.Users.Any(u => u.PhoneNumber == "1x34567" && u.Balance == 1000));

        _topUpGenieDbContext.Users.Remove(user);
        _topUpGenieDbContext.SaveChanges();

        var entity = _topUpGenieDbContext.Users.Find(user.Id);
        Assert.Null(entity);
    }
}

