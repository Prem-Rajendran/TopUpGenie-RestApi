using TopUpGenie.Common;
using TopUpGenie.Services.IntegrationTest.Helper;
using TopUpGenie.Services.IntegrationTest.Seeder;
using TopUpGenie.Services.Models.RequestModels;

namespace TopUpGenie.Services.IntegrationTest;

[Collection("SameClass")]
public class AdminServiceTests
{
    [Fact]
    public async Task AdminService_GetAllUsersAsync_Success()
    {
        DependencyProvider.SeedTopupGenieUsers();
        var response = await DependencyProvider.AdminService.GetAllUsersAsync(DependencyProvider.RequestContext);

        Assert.NotNull(response);
        Assert.NotNull(response.Data);
        Assert.True(response.Data.Any());
        Assert.Equal(Common.Enums.Status.Success, response.Status);
    }

    [Fact]
    public async Task AdminService_GetAllUsersAsync_Failure()
    {
        DependencyProvider.UnSeedTopupGenieUsers();
        var response = await DependencyProvider.AdminService.GetAllUsersAsync(DependencyProvider.RequestContext);

        Assert.NotNull(response);
        Assert.Null(response.Data);
        Assert.NotEqual(Common.Enums.Status.Success, response.Status);
        Assert.NotEmpty(response.Messages);
    }

    [Fact]
    public async Task AdminService_CreateUserAsync_Success()
    {
        var model = GetCreateUserModel(true);
        var response = await DependencyProvider.AdminService.CreateUserAsync(DependencyProvider.RequestContext, model);

        Assert.NotNull(response);
        Assert.NotNull(response.Data);
        Assert.Equal(Common.Enums.Status.Success, response.Status);
    }

    [Fact]
    public async Task AdminService_CreateUserAsync_Failure()
    {
        DependencyProvider.SeedTopupGenieUsers();

        var model = GetCreateUserModel(false);
        var response = await DependencyProvider.AdminService.CreateUserAsync(DependencyProvider.RequestContext, model);

        Assert.NotNull(response);
        Assert.Null(response.Data);
        Assert.NotEqual(Common.Enums.Status.Success, response.Status);
        Assert.NotEmpty(response.Messages);
    }

    [Fact]
    public async Task AdminService_DeleteUser_Success()
    {
        DependencyProvider.SeedTopupGenieUsers();
        var response = await DependencyProvider.AdminService.DeleteUser(DependencyProvider.RequestContext, 1);

        Assert.NotNull(response);
        Assert.True(response.Data);
        Assert.Equal(Common.Enums.Status.Success, response.Status);
    }

    [Fact]
    public async Task AdminService_DeleteUser_Failure()
    {
        DependencyProvider.UnSeedTopupGenieUsers();
        var response = await DependencyProvider.AdminService.DeleteUser(DependencyProvider.RequestContext, 1);

        Assert.NotNull(response);
        Assert.False(response.Data);
        Assert.NotEqual(Common.Enums.Status.Success, response.Status);
        Assert.NotEmpty(response.Messages);
    }

    [Fact]
    public async Task AdminService_GetUserByIdAsync_Success()
    {
        DependencyProvider.SeedTopupGenieUsers();
        var response = await DependencyProvider.AdminService.GetUserByIdAsync(DependencyProvider.RequestContext, 1);

        Assert.NotNull(response);
        Assert.NotNull(response.Data);
        Assert.Equal(Common.Enums.Status.Success, response.Status);
    }

    [Fact]
    public async Task AdminService_GetUserByIdAsync_Failure()
    {
        DependencyProvider.UnSeedTopupGenieUsers();
        var response = await DependencyProvider.AdminService.GetUserByIdAsync(DependencyProvider.RequestContext, 1);

        Assert.NotNull(response);
        Assert.Null(response.Data);
        Assert.NotEqual(Common.Enums.Status.Success, response.Status);
        Assert.NotEmpty(response.Messages);
    }

    [Fact]
    public async Task AdminService_UpdateUserAsync_Success()
    {
        DependencyProvider.SeedTopupGenieUsers();
        var model = GetUpdateUserModel(true, 2);
        var response = await DependencyProvider.AdminService.UpdateUserAsync(DependencyProvider.RequestContext, model);

        Assert.NotNull(response);
        Assert.True(response.Data);
        Assert.Equal(Common.Enums.Status.Success, response.Status);
    }

    [Fact]
    public async Task AdminService_UpdateUserAsync_Failure()
    {
        DependencyProvider.SeedTopupGenieUsers();
        var model = GetUpdateUserModel(false, 0);
        var response = await DependencyProvider.AdminService.UpdateUserAsync(DependencyProvider.RequestContext, model);

        Assert.NotNull(response);
        Assert.False(response.Data);
        Assert.NotEqual(Common.Enums.Status.Success, response.Status);
        Assert.NotEmpty(response.Messages);
    }

    [Fact]
    public async Task AdminService_GetLast5Transactions_Success()
    {
        DependencyProvider.SeedTransaction();
        var response = await DependencyProvider.AdminService.GetLast5Transactions();

        Assert.NotNull(response);
        Assert.NotNull(response.Data);
        Assert.True(response.Data.Any());
        Assert.Equal(Common.Enums.Status.Success, response.Status);
    }

    [Fact]
    public async Task AdminService_GetLast5Transactions_Failure()
    {
        DependencyProvider.UnSeedTopupGenieUsers();
        var response = await DependencyProvider.AdminService.GetLast5Transactions();

        Assert.NotNull(response);
        Assert.Null(response.Data);
        Assert.NotEqual(Common.Enums.Status.Success, response.Status);
        Assert.NotEmpty(response.Messages);
    }

    private static CreateUserRequestModel GetCreateUserModel(bool isValid)
    {
        if (isValid)
        {
            return new CreateUserRequestModel
            {
                Name = "test_user_123",
                IsVerified = true,
                Password = "1234",
                InitialBalance = 1000,
                PhoneNumber = "1122334"
            };
        }
        else return new CreateUserRequestModel();
    }

    private static UpdateUserRequestModel GetUpdateUserModel(bool isValid, int id)
    {
        if (isValid)
        {
            return new UpdateUserRequestModel
            {
                UserId = id,
                Name = "test_user_123_updated",
                IsVerified = true,
                OldPassword = "1234",
                NewPassword = "12345",
                ConfirmPassword = "12345",
                Money = 1000,
                PhoneNumber = "1122334"
            };
        }
        else return new UpdateUserRequestModel { UserId = id };
    }
}
