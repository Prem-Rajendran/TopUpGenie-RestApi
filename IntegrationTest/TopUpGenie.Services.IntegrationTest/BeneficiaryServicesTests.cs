using System;
using TopUpGenie.Services.IntegrationTest.Helper;
using TopUpGenie.Services.Models.RequestModels;

namespace TopUpGenie.Services.IntegrationTest
{
    [Collection("SameClass")]
    public class BeneficiaryServicesTests
	{
        [Fact]
        public async Task BeneficiaryServices_CreateBeneficiaryAsync_Success()
        {
            DependencyProvider.SeedTopupGenieUsers();
            var model = GetCreateBeneficiaryRequestModel(true);
            var context = DependencyProvider.RequestContext;
            context.UserId = 1;
            var response = await DependencyProvider.BeneficiaryService.CreateBeneficiaryAsync(context, model);
            DependencyProvider.UnSeedTopupGenieBeneficiary();

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal(Common.Enums.Status.Success, response.Status);
        }

        [Fact]
        public async Task BeneficiaryServices_CreateBeneficiaryAsync_Failure()
        {
            DependencyProvider.SeedTopupGenieUsers();
            var model = GetCreateBeneficiaryRequestModel(false);
            var response = await DependencyProvider.BeneficiaryService.CreateBeneficiaryAsync(DependencyProvider.RequestContext, model);

            Assert.NotNull(response);
            Assert.Null(response.Data);
            Assert.NotEqual(Common.Enums.Status.Success, response.Status);
            Assert.NotEmpty(response.Messages);
        }

        [Fact]
        public async Task BeneficiaryServices_GetAllAsync_Success()
        {
            DependencyProvider.SeedTopupGenieBeneficiary();
            var context = DependencyProvider.RequestContext;
            context.UserId = 1;
            var response = await DependencyProvider.BeneficiaryService.GetMyBeneficiaries(context);

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal(Common.Enums.Status.Success, response.Status);
        }

        [Fact]
        public async Task BeneficiaryServices_GetByIdAsync_Success()
        {
            DependencyProvider.SeedTopupGenieBeneficiary();
            var context = DependencyProvider.RequestContext;
            context.UserId = 1;
            var response = await DependencyProvider.BeneficiaryService.GetMyBeneficiaryById(context, 1);

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal(Common.Enums.Status.Success, response.Status);
        }

        [Fact]
        public async Task BeneficiaryServices_UpdateMyBeneficiary_Success()
        {
            DependencyProvider.SeedTopupGenieBeneficiary();
            var model = GetUpdateBeneficiaryRequestModel(true, 1);
            var context = DependencyProvider.RequestContext;
            context.UserId = 1;
            var response = await DependencyProvider.BeneficiaryService.UpdateMyBeneficiary(context, model);

            Assert.NotNull(response);
            Assert.True(response.Data);
            Assert.Equal(Common.Enums.Status.Success, response.Status);
        }

        [Fact]
        public async Task BeneficiaryServices_UpdateMyBeneficiary_Failure()
        {
            DependencyProvider.SeedTopupGenieUsers();
            var model = GetUpdateBeneficiaryRequestModel(false, 0);
            var response = await DependencyProvider.BeneficiaryService.UpdateMyBeneficiary(DependencyProvider.RequestContext, model);

            Assert.NotNull(response);
            Assert.False(response.Data);
            Assert.NotEqual(Common.Enums.Status.Success, response.Status);
            Assert.NotEmpty(response.Messages);
        }

        [Fact]
        public async Task BeneficiaryServices_ActivateMyBeneficiary_Success()
        {
            DependencyProvider.SeedTopupGenieBeneficiary();
            var context = DependencyProvider.RequestContext;
            context.UserId = 1;
            var response = await DependencyProvider.BeneficiaryService.ActivateMyBeneficiary(context, 1);

            Assert.NotNull(response);
            Assert.True(response.Data);
            Assert.Equal(Common.Enums.Status.Success, response.Status);
        }

        [Fact]
        public async Task BeneficiaryServices_ActivateMyBeneficiary_Failure()
        {
            DependencyProvider.SeedTopupGenieBeneficiary();
            var context = DependencyProvider.RequestContext;
            context.UserId = 1;
            var response = await DependencyProvider.BeneficiaryService.ActivateMyBeneficiary(context, 20);

            Assert.NotNull(response);
            Assert.False(response.Data);
            Assert.NotEqual(Common.Enums.Status.Success, response.Status);
            Assert.NotEmpty(response.Messages);
        }

        [Fact]
        public async Task BeneficiaryServices_DeActivateMyBeneficiary_Success()
        {
            DependencyProvider.SeedTopupGenieBeneficiary();
            var context = DependencyProvider.RequestContext;
            context.UserId = 1;
            var response = await DependencyProvider.BeneficiaryService.DeactivateMyBeneficiary(context, 1);

            Assert.NotNull(response);
            Assert.True(response.Data);
            Assert.Equal(Common.Enums.Status.Success, response.Status);
        }

        [Fact]
        public async Task BeneficiaryServices_DeActivateMyBeneficiary_Failure()
        {
            DependencyProvider.SeedTopupGenieBeneficiary();
            var context = DependencyProvider.RequestContext;
            context.UserId = 1;
            var response = await DependencyProvider.BeneficiaryService.DeactivateMyBeneficiary(context, 20);

            Assert.NotNull(response);
            Assert.False(response.Data);
            Assert.Equal(Common.Enums.Status.Failure, response.Status);
            Assert.NotEmpty(response.Messages);
        }

        [Fact]
        public async Task BeneficiaryServices_DeleteMyBeneficiary_Success()
        {
            DependencyProvider.SeedTopupGenieBeneficiary();
            var context = DependencyProvider.RequestContext;
            context.UserId = 1;
            var response = await DependencyProvider.BeneficiaryService.DeleteMyBeneficiary(context, 1);
            DependencyProvider.UnSeedTopupGenieBeneficiary();

            Assert.NotNull(response);
            Assert.True(response.Data);
            Assert.Equal(Common.Enums.Status.Success, response.Status);
        }

        [Fact]
        public async Task BeneficiaryServices_DeleteMyBeneficiary_Failure()
        {
            DependencyProvider.SeedTopupGenieBeneficiary();
            var context = DependencyProvider.RequestContext;
            context.UserId = 1;
            var response = await DependencyProvider.BeneficiaryService.DeleteMyBeneficiary(context, 20);
            DependencyProvider.UnSeedTopupGenieBeneficiary();

            Assert.NotNull(response);
            Assert.False(response.Data);
            Assert.Equal(Common.Enums.Status.Failure, response.Status);
            Assert.NotEmpty(response.Messages);
        }

        private static CreateBeneficiaryRequestModel GetCreateBeneficiaryRequestModel(bool isValid)
        {
            if (isValid)
            {
                return new CreateBeneficiaryRequestModel
                {
                    BeneficiaryNickname = "xyzabc",
                    BeneficiaryPhoneNumber = "1234567"
                };
            }
            else return new CreateBeneficiaryRequestModel();
        }

        private static UpdateBeneficiaryRequestModel GetUpdateBeneficiaryRequestModel(bool isValid, int id)
        {
            if (isValid)
            {
                return new UpdateBeneficiaryRequestModel
                {
                    BeneficiaryNickname = "xyzabc",
                    BeneficiaryPhoneNumber = "1234567",
                    BeneficiaryId = id
                };
            }
            else return new UpdateBeneficiaryRequestModel();
        }
    }
}

