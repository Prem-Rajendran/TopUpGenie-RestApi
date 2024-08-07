namespace TopUpGenie.DataAccess.IntegrationTest.RepositoryTests;

[Collection("Class")]
public class UserRepositoryTest
{
    [Theory]
    [InlineData("1234561")]
    [InlineData("1234562")]
    [InlineData("1234563")]
    public async Task UserRepository_GetUserByPhoneNumber_Success(string phoneNumber)
    {
        var dbContext = DependencyHelper.TopUpGenieDbContext;
        var repository = new UserRepository(dbContext, new NullLogger<Repository<User>>());

        var entity = await repository.GetUserByPhoneNumber(phoneNumber);
        Assert.NotNull(entity);
        Assert.True(entity.PhoneNumber == phoneNumber);
        dbContext.Dispose();
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("123")]
    [InlineData("123456678899")]
    public async Task UserRepository_GetUserByPhoneNumber_Failed(string phoneNumber)
    {
        var dbContext = DependencyHelper.TopUpGenieDbContext;
        var repository = new UserRepository(dbContext, new NullLogger<Repository<User>>());

        var entity = await repository.GetUserByPhoneNumber(phoneNumber);
        Assert.Null(entity);
        dbContext.Dispose();
    }
}

