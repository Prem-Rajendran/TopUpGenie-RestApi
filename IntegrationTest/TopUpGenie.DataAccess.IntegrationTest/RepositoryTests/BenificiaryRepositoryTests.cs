namespace TopUpGenie.DataAccess.IntegrationTest;

public class BenificiaryRepositoryTests
{
    [Fact]
    public async Task BenificiaryRepository_GetCountOfActiveBeneficiaries_Success()
    {
        var dbContext = DependencyHelper.TopUpGenieDbContext;
        var repository = new BenificiaryRepository(dbContext, new NullLogger<Repository<Beneficiary>>());

        var count = await repository.GetCountOfMyActiveBeneficiary(1);
        Assert.True(count > 0);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(11)]
    [InlineData(110)]
    public async Task BenificiaryRepository_GetCountOfActiveBeneficiaries_Failed(int id)
    {
        var dbContext = DependencyHelper.TopUpGenieDbContext;
        var repository = new BenificiaryRepository(dbContext, new NullLogger<Repository<Beneficiary>>());

        var count = await repository.GetCountOfMyActiveBeneficiary(id);
        Assert.False(count > 0);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task BenificiaryRepository_GetByIdAsync_Success(int id)
    {
        var dbContext = DependencyHelper.TopUpGenieDbContext;
        var repository = new BenificiaryRepository(dbContext, new NullLogger<Repository<Beneficiary>>());

        var beneficiary = await repository.GetByIdAsync(id);
        Assert.NotNull(beneficiary);
        Assert.True(beneficiary.Id == id);
    }
}

