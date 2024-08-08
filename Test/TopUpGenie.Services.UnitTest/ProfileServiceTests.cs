using TopUpGenie.Services.Constants;

namespace TopUpGenie.Services.UnitTest
{
    public class ProfileServiceTests
	{
		private readonly Mock<IUnitOfWork> _unitOfWork;
		private readonly Mock<ITransactionUnitOfWork> _transactionUnitOfWork;
		private readonly Mock<IUserRepository> _userRepository;
		private readonly Mock<IBeneficiaryRepository> _beneficiaryRepository;
		private readonly Mock<ITransactionRepository> _transactionRepository;
        private readonly IProfileService _profileService;

        public ProfileServiceTests()
		{
            _unitOfWork = new Mock<IUnitOfWork>();
            _transactionUnitOfWork = new Mock<ITransactionUnitOfWork>();
            _userRepository = new Mock<IUserRepository>();
            _beneficiaryRepository = new Mock<IBeneficiaryRepository>();
            _transactionRepository = new Mock<ITransactionRepository>();

            _unitOfWork.Setup(uow => uow.Users).Returns(_userRepository.Object);
            _unitOfWork.Setup(uow => uow.Beneficiaries).Returns(_beneficiaryRepository.Object);
            _transactionUnitOfWork.Setup(tuow => tuow.Transactions).Returns(_transactionRepository.Object);

            _profileService = new ProfileService(_unitOfWork.Object, _transactionUnitOfWork.Object);
        }

        [Fact]
        public async Task GetMyProfile_Success()
        {
            int userId = 1;
            var requestContext = DependencyProvider.RequestContext;
            requestContext.UserId = userId;

            var user = new User { Id = userId, Name = "Test User" };

            var beneficiaries = new List<Beneficiary>
            {
                new Beneficiary { Id = 1, Nickname = "Beneficiary 1", CreatedByUserId = userId },
                new Beneficiary { Id = 2, Nickname = "Beneficiary 2", CreatedByUserId = userId }
            };
            var transactions = new List<Transaction>
            {
                new Transaction { Id = 1, TransactionAmount = 100, UserId = userId },
                new Transaction { Id = 2, TransactionAmount = 200, UserId = userId }
            };

            _userRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);
            _beneficiaryRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(beneficiaries);
            _transactionRepository.Setup(repo => repo.GetUsersMonthlyTransactions(userId)).ReturnsAsync(transactions);

            var response = await _profileService.GetMyProfile(requestContext);

            Assert.NotNull(response);
            Assert.Equal(Common.Enums.Status.Success, response.Status);
            Assert.NotNull(response.Data);
            Assert.NotNull(response.Data.UserDetails);
            Assert.NotNull(response.Data.Beneficiaries);
            Assert.NotNull(response.Data.LastFiveTransactions);
            Assert.Equal(userId, response.Data.UserDetails.UserId);
            Assert.Equal(beneficiaries.Count, response.Data.Beneficiaries.Count());
            Assert.Equal(transactions.Count, response.Data.LastFiveTransactions.Count());
        }

        [Fact]
        public async Task GetMyProfile_UserNotFound()
        {
            int userId = 1;
            var requestContext = DependencyProvider.RequestContext;
            requestContext.UserId = userId;

            _userRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync((User?)null);

            var response = await _profileService.GetMyProfile(requestContext);

            Assert.NotNull(response);
            Assert.Equal(Common.Enums.Status.Failure, response.Status);
            Assert.NotNull(response.Messages);
            Assert.Contains(response.Messages, m => m.ErrorCode == ErrorCodes.PROFILE_GET_FAILED);
        }

        [Fact]
        public async Task GetMyProfile_NoTransactions()
        {
            int userId = 1;
            var requestContext = DependencyProvider.RequestContext;
            requestContext.UserId = userId;

            var user = new User { Id = userId, Name = "Test User" };
            var beneficiaries = new List<Beneficiary>
            {
                new Beneficiary { Id = 1, Nickname = "Beneficiary 1", CreatedByUserId = userId },
                new Beneficiary { Id = 2, Nickname = "Beneficiary 2", CreatedByUserId = userId }
            };

            _userRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);
            _beneficiaryRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(beneficiaries);
            _transactionRepository.Setup(repo => repo.GetUsersMonthlyTransactions(userId)).ReturnsAsync(new List<Transaction>());

            var response = await _profileService.GetMyProfile(requestContext);

            Assert.NotNull(response);
            Assert.Equal(Common.Enums.Status.Success, response.Status);
            Assert.NotNull(response.Data);
            Assert.NotNull(response.Data.UserDetails);
            Assert.Equal(userId, response.Data.UserDetails.UserId);
            Assert.Equal(beneficiaries.Count, response.Data.Beneficiaries.Count());
            Assert.Null(response.Data.LastFiveTransactions);
        }

        [Fact]
        public async Task GetMyProfile_NoBeneficiaries()
        {
            int userId = 1;
            var requestContext = DependencyProvider.RequestContext;
            requestContext.UserId = userId;

            var user = new User { Id = userId, Name = "Test User" };

            var transactions = new List<Transaction>
            {
                new Transaction { Id = 1, TransactionAmount = 100, UserId = userId },
                new Transaction { Id = 2, TransactionAmount = 200, UserId = userId }
            };

            _userRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);
            _beneficiaryRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Beneficiary>());
            _transactionRepository.Setup(repo => repo.GetUsersMonthlyTransactions(userId)).ReturnsAsync(transactions);

            var response = await _profileService.GetMyProfile(requestContext);

            Assert.NotNull(response);
            Assert.Equal(Common.Enums.Status.Success, response.Status);
            Assert.NotNull(response.Data);
            Assert.NotNull(response.Data.UserDetails);
            Assert.Null(response.Data.Beneficiaries);
            Assert.Equal(userId, response.Data.UserDetails.UserId);
        }

        [Fact]
        public async Task GetMyProfile_ExceptionThrown()
        {
            int userId = 1;
            var requestContext = DependencyProvider.RequestContext;
            requestContext.UserId = userId;

            _userRepository.Setup(repo => repo.GetByIdAsync(userId)).ThrowsAsync(new Exception("Database error"));

            var response = await _profileService.GetMyProfile(requestContext);

            Assert.NotNull(response);
            Assert.Equal(Common.Enums.Status.Unknown, response.Status);
            Assert.NotNull(response.Messages);
            Assert.Contains(response.Messages, m => m.ErrorCode == ErrorCodes.PROFILE_GET_EXCEPTION);
        }

    }
}