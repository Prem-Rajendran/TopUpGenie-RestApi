namespace TopUpGenie.Services.UnitTest
{
	public class BeneficiaryServiceTests
	{
		private static IBeneficiaryService? _beneficiaryService;
		private readonly Mock<IUnitOfWork> _unitOfWork;
		private readonly Mock<IBeneficiaryRepository> _beneficiaryRepository;
        private readonly Mock<IUserRepository> _userRepository;


        public BeneficiaryServiceTests()
		{
            _unitOfWork = new Mock<IUnitOfWork>();
			_beneficiaryRepository = new Mock<IBeneficiaryRepository>();
            _userRepository = new Mock<IUserRepository>();

            _unitOfWork.Setup(uow => uow.Beneficiaries).Returns(_beneficiaryRepository.Object);
            _unitOfWork.Setup(uow => uow.Users).Returns(_userRepository.Object);
            _unitOfWork.Setup(uow => uow.CompleteAsync()).ReturnsAsync(true);

            _userRepository.Setup(u => u.GetUserByPhoneNumber(It.IsAny<string>())).ReturnsAsync(new DataAccess.DataModel.User() { Id = 1, Name = "User", PhoneNumber = "1234567" });
            _beneficiaryRepository.Setup(u => u.AddAsync(It.IsAny<Beneficiary>())).ReturnsAsync(true);
            _beneficiaryRepository.Setup(u => u.GetCountOfMyActiveBeneficiary(It.IsAny<int>())).ReturnsAsync(1);
            _beneficiaryRepository.Setup(u => u.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Beneficiary { CreatedByUserId = 1 });
            _beneficiaryRepository.Setup(u => u.Update(It.IsAny<Beneficiary>())).Returns(true);
            _beneficiaryRepository.Setup(u => u.GetAllAsync()).ReturnsAsync(new List<Beneficiary>() { new Beneficiary() });
            _beneficiaryRepository.Setup(u => u.Delete(It.IsAny<Beneficiary>())).Returns(true);
        }

        #region CreateBeneficiaryAsync
        [Fact]
        public async Task CreateBeneficiaryAsync_Success()
        {
            _beneficiaryService = new BeneficiaryService(_unitOfWork.Object);

            var response = await _beneficiaryService.CreateBeneficiaryAsync(
                DependencyProvider.RequestContext, new CreateBeneficiaryRequestModel { BeneficiaryNickname = "Test", BeneficiaryPhoneNumber = "1234567"});

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal(Common.Enums.Status.Success, response.Status);
        }

        [Fact]
        public async Task CreateBeneficiaryAsync_Failure()
        {
            _unitOfWork.Setup(uow => uow.CompleteAsync()).ReturnsAsync(false);
            _beneficiaryService = new BeneficiaryService(_unitOfWork.Object);

            var response = await _beneficiaryService.CreateBeneficiaryAsync(
                DependencyProvider.RequestContext, new CreateBeneficiaryRequestModel { BeneficiaryNickname = "Test", BeneficiaryPhoneNumber = "1234567" });

            Assert.NotNull(response);
            Assert.Null(response.Data);
            Assert.NotEqual(Common.Enums.Status.Success, response.Status);
            Assert.NotEmpty(response.Messages);
        }

        [Fact]
        public async Task CreateBeneficiaryAsync_Failure_MaxLimitExceeded()
        {
            _beneficiaryRepository.Setup(u => u.GetCountOfMyActiveBeneficiary(It.IsAny<int>())).ReturnsAsync(10);
            _beneficiaryService = new BeneficiaryService(_unitOfWork.Object);

            var response = await _beneficiaryService.CreateBeneficiaryAsync(
                DependencyProvider.RequestContext, new CreateBeneficiaryRequestModel { BeneficiaryNickname = "Test", BeneficiaryPhoneNumber = "1234567" });

            Assert.NotNull(response);
            Assert.Null(response.Data);
            Assert.NotEqual(Common.Enums.Status.Success, response.Status);
        }

        [Fact]
        public async Task CreateBeneficiaryAsync_Exception()
        {
            _unitOfWork.Setup(uow => uow.CompleteAsync()).ThrowsAsync(new Exception("Exception"));
            _beneficiaryService = new BeneficiaryService(_unitOfWork.Object);

            var response = await _beneficiaryService.CreateBeneficiaryAsync(
                DependencyProvider.RequestContext, new CreateBeneficiaryRequestModel { BeneficiaryNickname = "Test", BeneficiaryPhoneNumber = "1234567" });

            Assert.NotNull(response);
            Assert.Null(response.Data);
            Assert.NotEqual(Common.Enums.Status.Success, response.Status);
            Assert.NotEmpty(response.Messages);
        }
        #endregion

        #region GetMyBeneficiaryById
        [Fact]
        public async Task GetMyBeneficiaryById_Success()
        {
            _beneficiaryService = new BeneficiaryService(_unitOfWork.Object);

            var context = DependencyProvider.RequestContext;
            context.UserId = 1;
            var response = await _beneficiaryService.GetMyBeneficiaryById(context, 1);

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal(Common.Enums.Status.Success, response.Status);
        }

        [Fact]
        public async Task GetMyBeneficiaryById_Failure()
        {
            _beneficiaryService = new BeneficiaryService(_unitOfWork.Object);

            var context = DependencyProvider.RequestContext;
            context.UserId = 0;
            var response = await _beneficiaryService.GetMyBeneficiaryById(context, 1);

            Assert.NotNull(response);
            Assert.Null(response.Data);
            Assert.NotEqual(Common.Enums.Status.Success, response.Status);
            Assert.NotEmpty(response.Messages);
        }

        [Fact]
        public async Task GetMyBeneficiaryById_Exception()
        {
            _beneficiaryRepository.Setup(u => u.GetByIdAsync(It.IsAny<int>())).Throws(new Exception("Exception"));
            _beneficiaryService = new BeneficiaryService(_unitOfWork.Object);

            var context = DependencyProvider.RequestContext;
            context.UserId = 0;
            var response = await _beneficiaryService.GetMyBeneficiaryById(context, 1);

            Assert.NotNull(response);
            Assert.Null(response.Data);
            Assert.NotEqual(Common.Enums.Status.Success, response.Status);
            Assert.NotEmpty(response.Messages);
        }

        #endregion

        #region ActivateMyBeneficiary
        [Fact]
        public async Task ActivateMyBeneficiary_Success()
        {
            _beneficiaryService = new BeneficiaryService(_unitOfWork.Object);

            var context = DependencyProvider.RequestContext;
            context.UserId = 1;
            var response = await _beneficiaryService.ActivateMyBeneficiary(context, 1);

            Assert.NotNull(response);
            Assert.True(response.Data);
            Assert.Equal(Common.Enums.Status.Success, response.Status);
        }

        [Fact]
        public async Task ActivateMyBeneficiary_Failure()
        {
            _beneficiaryService = new BeneficiaryService(_unitOfWork.Object);

            var context = DependencyProvider.RequestContext;
            context.UserId = 0;
            var response = await _beneficiaryService.ActivateMyBeneficiary(context, 1);

            Assert.NotNull(response);
            Assert.False(response.Data);
            Assert.NotEqual(Common.Enums.Status.Success, response.Status);
            Assert.NotEmpty(response.Messages);
        }

        [Fact]
        public async Task ActivateMyBeneficiary_Update_Failure()
        {
            _beneficiaryRepository.Setup(u => u.Update(It.IsAny<Beneficiary>())).Returns(false);
            _beneficiaryService = new BeneficiaryService(_unitOfWork.Object);

            var context = DependencyProvider.RequestContext;
            context.UserId = 0;
            var response = await _beneficiaryService.ActivateMyBeneficiary(context, 1);

            Assert.NotNull(response);
            Assert.False(response.Data);
            Assert.NotEqual(Common.Enums.Status.Success, response.Status);
            Assert.NotEmpty(response.Messages);
        }

        [Fact]
        public async Task ActivateMyBeneficiary_Exception()
        {
            _beneficiaryRepository.Setup(u => u.GetByIdAsync(It.IsAny<int>())).Throws(new Exception("Exception"));
            _beneficiaryService = new BeneficiaryService(_unitOfWork.Object);

            var context = DependencyProvider.RequestContext;
            context.UserId = 0;
            var response = await _beneficiaryService.ActivateMyBeneficiary(context, 1);

            Assert.NotNull(response);
            Assert.False(response.Data);
            Assert.NotEqual(Common.Enums.Status.Success, response.Status);
            Assert.NotEmpty(response.Messages);
        }
        #endregion

        #region DeactivateMyBeneficiary
        [Fact]
        public async Task DeactivateMyBeneficiary()
        {
            _beneficiaryService = new BeneficiaryService(_unitOfWork.Object);

            var context = DependencyProvider.RequestContext;
            context.UserId = 1;
            var response = await _beneficiaryService.DeactivateMyBeneficiary(context, 1);

            Assert.NotNull(response);
            Assert.True(response.Data);
            Assert.Equal(Common.Enums.Status.Success, response.Status);
        }

        #endregion

        #region GetMyBeneficiaries

        [Fact]
        public async Task GetMyBeneficiaries_Success()
        {
            _beneficiaryService = new BeneficiaryService(_unitOfWork.Object);

            var context = DependencyProvider.RequestContext;
            context.UserId = 1;
            var response = await _beneficiaryService.GetMyBeneficiaries(context);

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal(Common.Enums.Status.Success, response.Status);
        }

        [Fact]
        public async Task GetMyBeneficiaries_Failed()
        {
            _beneficiaryRepository.Setup(u => u.GetAllAsync()).ReturnsAsync(new List<Beneficiary>());
            _beneficiaryService = new BeneficiaryService(_unitOfWork.Object);

            var context = DependencyProvider.RequestContext;
            context.UserId = 0;
            var response = await _beneficiaryService.GetMyBeneficiaries(context);

            Assert.NotNull(response);
            Assert.Null(response.Data);
            Assert.NotEqual(Common.Enums.Status.Success, response.Status);
            Assert.NotEmpty(response.Messages);
        }

        [Fact]
        public async Task GetMyBeneficiaries_Exception()
        {
            _beneficiaryRepository.Setup(u => u.GetAllAsync()).Throws(new Exception("Exception"));
            _beneficiaryService = new BeneficiaryService(_unitOfWork.Object);

            var context = DependencyProvider.RequestContext;
            context.UserId = 0;
            var response = await _beneficiaryService.GetMyBeneficiaries(context);

            Assert.NotNull(response);
            Assert.Null(response.Data);
            Assert.NotEqual(Common.Enums.Status.Success, response.Status);
            Assert.NotEmpty(response.Messages);
        }

        #endregion

        #region DeleteMyBeneficiary

        [Fact]
        public async Task DeleteMyBeneficiary_Succcess()
        {
            _beneficiaryService = new BeneficiaryService(_unitOfWork.Object);

            var context = DependencyProvider.RequestContext;
            context.UserId = 1;
            var response = await _beneficiaryService.DeleteMyBeneficiary(context, 1);

            Assert.NotNull(response);
            Assert.True(response.Data);
            Assert.Equal(Common.Enums.Status.Success, response.Status);
        }

        [Fact]
        public async Task DeleteMyBeneficiary_Failed()
        {
            _beneficiaryRepository.Setup(u => u.Delete(It.IsAny<Beneficiary>())).Returns(false);
            _beneficiaryService = new BeneficiaryService(_unitOfWork.Object);

            var context = DependencyProvider.RequestContext;
            context.UserId = 0;
            var response = await _beneficiaryService.DeleteMyBeneficiary(context, 1);

            Assert.NotNull(response);
            Assert.False(response.Data);
            Assert.NotEqual(Common.Enums.Status.Success, response.Status);
            Assert.NotEmpty(response.Messages);
        }

        [Fact]
        public async Task DeleteMyBeneficiary_Exception()
        {
            _beneficiaryRepository.Setup(u => u.Delete(It.IsAny<Beneficiary>())).Throws(new Exception("Exception"));
            _beneficiaryService = new BeneficiaryService(_unitOfWork.Object);

            var context = DependencyProvider.RequestContext;
            context.UserId = 0;
            var response = await _beneficiaryService.DeleteMyBeneficiary(context, 1);

            Assert.NotNull(response);
            Assert.False(response.Data);
            Assert.NotEqual(Common.Enums.Status.Success, response.Status);
            Assert.NotEmpty(response.Messages);
        }

        #endregion
    }
}

