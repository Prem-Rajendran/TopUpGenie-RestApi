using TopUpGenie.Services.IntegrationTest.Helper;
using TopUpGenie.Services.Models.RequestModels;

namespace TopUpGenie.Services.IntegrationTest;


[Collection("SameClass")]
public class TopUpServiceTests
{
	public TopUpServiceTests()
	{
		DependencyProvider.UnSeedTopupGenieUsers();
		DependencyProvider.UnSeedTransactionUser();
		DependencyProvider.SeedTransaction();
        DependencyProvider.SeedTopupGenieTransaction();
    }

    [Fact]
    public async Task TopUpService_GetTopUpOptions_Success()
    {
        var response = await DependencyProvider.TopUpService.ListTopUpOptions();

        Assert.NotNull(response);
        Assert.NotNull(response.Data);
        Assert.True(response.Data.Any());
        Assert.Equal(Common.Enums.Status.Success, response.Status);
    }

    [Fact]
    public async Task TopUpService_GetTopUpOptions_Failure()
    {
        DependencyProvider.UnSeedTopupGenieTopUpOptions();
        var response = await DependencyProvider.TopUpService.ListTopUpOptions();

        Assert.NotNull(response);
        Assert.Null(response.Data);
        Assert.NotEqual(Common.Enums.Status.Success, response.Status);
    }

    [Fact]
    public async Task TopUpService_TopUpTransaction_Success()
    {
        DependencyProvider.UnSeedTopupGenieTransaction();
        DependencyProvider.UnSeedTransaction();

        var context = DependencyProvider.RequestContext;
        context.UserId = 1;
        var response = await DependencyProvider.TopUpService.TopUpTransaction(context, GetRequestModel(true));

        Assert.NotNull(response);
        Assert.True(response.Data);
        Assert.Equal(Common.Enums.Status.Success, response.Status);
    }

    [Fact]
    public async Task TopUpService_TopUpTransaction_Failure_MonthlyLimitCrossed()
    {
        var context = DependencyProvider.RequestContext;
        context.UserId = 1;
        var response = await DependencyProvider.TopUpService.TopUpTransaction(context, GetRequestModel(true));

        Assert.NotNull(response);
        Assert.False(response.Data);
        Assert.NotEqual(Common.Enums.Status.Success, response.Status);
        Assert.NotEmpty(response.Messages);
    }

    [Fact]
    public async Task TopUpService_TopUpTransaction_Failure_UserLowBalance()
    {
        DependencyProvider.UnSeedTopupGenieTransaction();
        DependencyProvider.UnSeedTransaction();

        var context = DependencyProvider.RequestContext;
        context.UserId = 1;
        _ = await DependencyProvider.AdminService.UpdateUserAsync(context, GetUserRequestModel());
        var response = await DependencyProvider.TopUpService.TopUpTransaction(context, GetRequestModel(true));

        Assert.NotNull(response);
        Assert.False(response.Data);
        Assert.NotEqual(Common.Enums.Status.Success, response.Status);
        Assert.NotEmpty(response.Messages);
    }

    [Fact]
    public async Task TopUpService_TopUpTransaction_Failure()
    {
        var context = DependencyProvider.RequestContext;
        context.UserId = 1;
        var response = await DependencyProvider.TopUpService.TopUpTransaction(context, GetRequestModel(false));

        Assert.NotNull(response);
        Assert.False(response.Data);
        Assert.NotEqual(Common.Enums.Status.Success, response.Status);
        Assert.NotEmpty(response.Messages);
    }

    private static UpdateUserRequestModel GetUserRequestModel()
    {
        return new UpdateUserRequestModel
        {
            UserId = 1,
            Money = -999999
        };
    }

    private static InitiateTransactionRequestModel GetRequestModel(bool isValid)
    {
        if (isValid)
        {
            return new InitiateTransactionRequestModel
            {
                BeneficiaryId = 2,
                TopUpOptionId = 6
            };
        }
        return new InitiateTransactionRequestModel
        {
            BeneficiaryId = 2,
            TopUpOptionId = 100
        };
    }
}

