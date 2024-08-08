using System;
using TopUpGenie.Services.IntegrationTest.Helper;
using TopUpGenie.Services.Models.RequestModels;

namespace TopUpGenie.Services.IntegrationTest
{
    [Collection("SameClass")]
    public class AuthServiceTests
	{
        public AuthServiceTests()
        {
            DependencyProvider.UnSeedTopupGenieUsers();
        }

        [Fact]
        public async Task AuthService_AuthenticateAsync_Success()
        {
            DependencyProvider.SeedTopupGenieUsers();

            var model = GetRequestModel(true);
            var response = await DependencyProvider.AuthService.AuthenticateAsync(DependencyProvider.RequestContext, model);

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal(Common.Enums.Status.Success, response.Status);
        }

        [Fact]
        public async Task AuthService_AuthenticateAsync_ForceTerminateSession_Success()
        {
            DependencyProvider.SeedTopupGenieUsers();

            var model = GetRequestModel(true);
            var response = await DependencyProvider.AuthService.AuthenticateAsync(DependencyProvider.RequestContext, model);
            response = await DependencyProvider.AuthService.AuthenticateAsync(DependencyProvider.RequestContext, model);

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal(Common.Enums.Status.Success, response.Status);
            Assert.NotEmpty(response.Messages);
        }

        [Fact]
        public async Task AuthService_AuthenticateAsync_Failure()
        {
            DependencyProvider.SeedTopupGenieUsers();

            var model = GetRequestModel(false);
            var response = await DependencyProvider.AuthService.AuthenticateAsync(DependencyProvider.RequestContext, model);

            Assert.NotNull(response);
            Assert.Null(response.Data);
            Assert.NotEqual(Common.Enums.Status.Success, response.Status);
            Assert.NotEmpty(response.Messages);
        }

        [Fact]
        public async Task AuthService_InvalidateTokenAsync_Success()
        {
            DependencyProvider.SeedTopupGenieUsers();

            var model = GetRequestModel(true);
            _ = await DependencyProvider.AuthService.AuthenticateAsync(DependencyProvider.RequestContext, model);
            var context = DependencyProvider.RequestContext;
            context.UserId = 1;
            var response = await DependencyProvider.AuthService.InvalidateTokenAsync(DependencyProvider.RequestContext);

            Assert.NotNull(response);
            Assert.True(response.Data);
            Assert.Equal(Common.Enums.Status.Success, response.Status);
        }

        [Fact]
        public async Task AuthService_InvalidateTokenAsync_Failure()
        {
            DependencyProvider.SeedTopupGenieUsers();
            DependencyProvider.RequestContext.AccessToken = "";
            var response = await DependencyProvider.AuthService.InvalidateTokenAsync(DependencyProvider.RequestContext);

            Assert.NotNull(response);
            Assert.False(response.Data);
            Assert.NotEqual(Common.Enums.Status.Success, response.Status);
            Assert.NotEmpty(response.Messages);
        }

        private static UserAuthenticationRequestModel GetRequestModel(bool isValid)
        {
            if (isValid)
            {
                return new UserAuthenticationRequestModel
                {
                    UserId = 1,
                    Password = "1234"
                };
            }
            else
            {
                return new UserAuthenticationRequestModel
                {
                    UserId = 1,
                    Password = ""
                };
            }
        }
    }
}

