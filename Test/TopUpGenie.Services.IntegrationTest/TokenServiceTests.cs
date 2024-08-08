using TopUpGenie.Common;
using TopUpGenie.Common.Interface;
using TopUpGenie.Services.IntegrationTest.Helper;
using TopUpGenie.Services.Models.ResponseModels;

namespace TopUpGenie.Services.IntegrationTest;

[Collection("SameClass")]
public class TokenServiceTests
{
	public TokenServiceTests()
	{
		DependencyProvider.UnSeedTopupGenieUsers();
		DependencyProvider.UnSeedTransactionUser();
		DependencyProvider.UnSeedTopupGenieLoginSession();
	}

    [Fact]
    public async Task TokenService_ValidateToken_Success_1()
    {
        string accessToken = DependencyProvider.SeedTopupGenieLoginSession()[0]; 
        var response = await DependencyProvider.TokenService.ValidateToken(new GenericServiceResponse<ValidateTokenRequestModel>(), accessToken);

        Assert.NotNull(response);
        Assert.NotNull(response.Data);
        Assert.Equal(Common.Enums.Status.Success, response.Status);
    }

    [Fact]
    public async Task TokenService_ValidateToken_Success_2()
    {
        string accessToken = DependencyProvider.SeedTopupGenieLoginSession()[1];
        var response = await DependencyProvider.TokenService.ValidateToken(new GenericServiceResponse<ValidateTokenRequestModel>(), accessToken);

        Assert.NotNull(response);
        Assert.NotNull(response.Data);
        Assert.Equal(Common.Enums.Status.Success, response.Status);
    }

    [Fact]
    public async Task TokenService_ValidateToken_Failure_EmptyToken()
    {
        var response = await DependencyProvider.TokenService.ValidateToken(new GenericServiceResponse<ValidateTokenRequestModel>(), "");

        Assert.NotNull(response);
        Assert.Null(response.Data);
        Assert.NotEmpty(response.Messages);
    }

    [Fact]
    public async Task TokenService_ValidateToken_Failure_InvalidExpiredToken()
    {
        string accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwidW5pcXVlX25hbWUiOiJVc2VyMCIsInJvbGUiOiJ1c2VyIiwibmJmIjoxNzIzMDg5MjY2LCJleHAiOjE3MjMwODkzMjYsImlhdCI6MTcyMzA4OTI2NiwiaXNzIjoiVG9wVXBHZW5pZUlzc3VlciIsImF1ZCI6IlRvcFVwR2VuaWVBdWRpZW5jZSJ9.O81QJRQNyysU7LidaujN_3qre27W6SgDk3ubPsWy4wU";
        var response = await DependencyProvider.TokenService.ValidateToken(new GenericServiceResponse<ValidateTokenRequestModel>(), accessToken);

        Assert.NotNull(response);
        Assert.NotNull(response.Data);
        Assert.NotEqual(Common.Enums.Status.Success, response.Status);
    }
}