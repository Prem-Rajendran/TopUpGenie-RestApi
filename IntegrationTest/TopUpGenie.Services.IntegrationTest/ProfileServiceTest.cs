using System;
using TopUpGenie.Services.IntegrationTest.Helper;

namespace TopUpGenie.Services.IntegrationTest
{
    [Collection("SameClass")]
    public class ProfileServiceTest
	{
		public ProfileServiceTest()
		{
            DependencyProvider.UnSeedTopupGenieUsers();
            DependencyProvider.UnSeedTransactionUser();
        }

        [Fact]
        public async Task ProfileService_AuthenticateAsync_Success()
        {
            DependencyProvider.SeedTransaction();
            DependencyProvider.SeedTopupGenieTransaction();

            var context = DependencyProvider.RequestContext;
            context.UserId = 1;
            var response = await DependencyProvider.ProfileService.GetMyProfile(context);

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal(Common.Enums.Status.Success, response.Status);
        }

        [Fact]
        public async Task ProfileService_AuthenticateAsync_Success_1()
        {
            DependencyProvider.SeedTransaction();
            DependencyProvider.SeedTopupGenieTransaction();
            DependencyProvider.UnSeedTopupGenieTransaction();
            DependencyProvider.UnSeedTransaction();

            var context = DependencyProvider.RequestContext;
            context.UserId = 1;
            var response = await DependencyProvider.ProfileService.GetMyProfile(context);

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal(Common.Enums.Status.Success, response.Status);
            Assert.NotEmpty(response.Messages);
        }

        [Fact]
        public async Task ProfileService_AuthenticateAsync_Success_2()
        {
            DependencyProvider.SeedTransaction();
            DependencyProvider.SeedTopupGenieTransaction();
            DependencyProvider.UnSeedTopupGenieTransaction();
            DependencyProvider.UnSeedTransaction();
            DependencyProvider.UnSeedTransactionBeneficiary();
            DependencyProvider.UnSeedTopupGenieBeneficiary();

            var context = DependencyProvider.RequestContext;
            context.UserId = 1;
            var response = await DependencyProvider.ProfileService.GetMyProfile(context);

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal(Common.Enums.Status.Success, response.Status);
            Assert.NotEmpty(response.Messages);
        }

        [Fact]
        public async Task ProfileService_AuthenticateAsync_Failure()
        {
            DependencyProvider.SeedTransaction();
            DependencyProvider.SeedTopupGenieTransaction();
            DependencyProvider.UnSeedTopupGenieTransaction();
            DependencyProvider.UnSeedTransaction();
            DependencyProvider.UnSeedTransactionBeneficiary();
            DependencyProvider.UnSeedTopupGenieBeneficiary();

            var context = DependencyProvider.RequestContext;
            context.UserId = 0;
            var response = await DependencyProvider.ProfileService.GetMyProfile(context);

            Assert.NotNull(response);
            Assert.Null(response.Data);
            Assert.NotEqual(Common.Enums.Status.Success, response.Status);
            Assert.NotEmpty(response.Messages);
        }
    }
}

