using TopUpGenie.Common;
using TopUpGenie.Services.Extensions;
using TopUpGenie.Services.Models.Dto;

namespace TopUpGenie.Services.UnitTest
{
	public class AdminServiceTests
	{
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<ITransactionUnitOfWork> _transactionUnitOfWork;
        private readonly IAdminService _adminService;

        public AdminServiceTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _transactionUnitOfWork = new Mock<ITransactionUnitOfWork>();

            _adminService = new AdminService(_unitOfWork.Object, _transactionUnitOfWork.Object);
        }

        [Fact]
        public async Task CreateUserAsync_Success()
        {
            var requestModel = new CreateUserRequestModel {};
            var user = requestModel.ToNewUser();
            var response = new GenericServiceResponse<UserDto> { Status = Common.Enums.Status.Unknown };

            _unitOfWork.Setup(uow => uow.Users.AddAsync(It.IsAny<User>())).ReturnsAsync(true);
            _unitOfWork.Setup(uow => uow.CompleteAsync()).ReturnsAsync(true);

            var result = await _adminService.CreateUserAsync(DependencyProvider.RequestContext, requestModel);

            Assert.Equal(Common.Enums.Status.Success, result.Status);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task CreateUserAsync_Failure()
        {
            
            var requestModel = new CreateUserRequestModel {};
            var user = requestModel.ToNewUser();
            var response = new GenericServiceResponse<UserDto> { Status = Common.Enums.Status.Unknown };

            _unitOfWork.Setup(uow => uow.Users.AddAsync(It.IsAny<User>())).ReturnsAsync(false);

            
            var result = await _adminService.CreateUserAsync(DependencyProvider.RequestContext, requestModel);

            
            Assert.Equal(Common.Enums.Status.Failure, result.Status);
            Assert.Contains(result.Messages, m => m.ErrorCode == ErrorCodes.ADMIN_CREATE_USER_FAILED);
        }

        [Fact]
        public async Task CreateUserAsync_ExceptionThrown()
        {
            
            var requestModel = new CreateUserRequestModel {};
            var response = new GenericServiceResponse<UserDto> { Status = Common.Enums.Status.Unknown };

            _unitOfWork.Setup(uow => uow.Users.AddAsync(It.IsAny<User>())).ThrowsAsync(new Exception("Database error"));

            
            var result = await _adminService.CreateUserAsync(DependencyProvider.RequestContext, requestModel);

            
            Assert.Equal(Common.Enums.Status.Unknown, result.Status);
            Assert.Contains(result.Messages, m => m.ErrorCode == ErrorCodes.ADMIN_CREATE_USER_EXCEPTION);
        }

        [Fact]
        public async Task GetAllUsersAsync_Success()
        {
            
            var users = new List<User> { new User {} };
            var response = new GenericServiceResponse<IEnumerable<UserDto>> { Status = Common.Enums.Status.Unknown };

            _unitOfWork.Setup(uow => uow.Users.GetAllAsync()).ReturnsAsync(users);

            
            var result = await _adminService.GetAllUsersAsync(DependencyProvider.RequestContext);

            
            Assert.Equal(Common.Enums.Status.Success, result.Status);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task GetAllUsersAsync_Failure()
        {
            
            var response = new GenericServiceResponse<IEnumerable<UserDto>> { Status = Common.Enums.Status.Unknown };

            _unitOfWork.Setup(uow => uow.Users.GetAllAsync()).ReturnsAsync((IEnumerable<User>)null);

            
            var result = await _adminService.GetAllUsersAsync(DependencyProvider.RequestContext);

            
            Assert.Equal(Common.Enums.Status.Failure, result.Status);
            Assert.Contains(result.Messages, m => m.ErrorCode == ErrorCodes.ADMIN_GET_ALL_USERS_FAILED);
        }

        [Fact]
        public async Task GetAllUsersAsync_ExceptionThrown()
        {
            
            var response = new GenericServiceResponse<IEnumerable<UserDto>> { Status = Common.Enums.Status.Unknown };

            _unitOfWork.Setup(uow => uow.Users.GetAllAsync()).ThrowsAsync(new Exception("Database error"));

            
            var result = await _adminService.GetAllUsersAsync(DependencyProvider.RequestContext);

            
            Assert.Equal(Common.Enums.Status.Unknown, result.Status);
            Assert.Contains(result.Messages, m => m.ErrorCode == ErrorCodes.ADMIN_GET_ALL_USERS_EXCEPTION);
        }

        [Fact]
        public async Task GetUserByIdAsync_Success()
        {
            
            var user = new User {};
            var response = new GenericServiceResponse<UserDto> { Status = Common.Enums.Status.Unknown };

            _unitOfWork.Setup(uow => uow.Users.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(user);

            
            var result = await _adminService.GetUserByIdAsync(DependencyProvider.RequestContext, 1);

            
            Assert.Equal(Common.Enums.Status.Success, result.Status);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task GetUserByIdAsync_Failure()
        {
            
            var response = new GenericServiceResponse<UserDto> { Status = Common.Enums.Status.Unknown };

            _unitOfWork.Setup(uow => uow.Users.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((User)null);

            
            var result = await _adminService.GetUserByIdAsync(DependencyProvider.RequestContext, 1);

            
            Assert.Equal(Common.Enums.Status.Failure, result.Status);
            Assert.Contains(result.Messages, m => m.ErrorCode == ErrorCodes.ADMIN_GET_USER_BY_ID_FAILED);
        }

        [Fact]
        public async Task GetUserByIdAsync_ExceptionThrown()
        {
            
            var response = new GenericServiceResponse<UserDto> { Status = Common.Enums.Status.Unknown };

            _unitOfWork.Setup(uow => uow.Users.GetByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("Database error"));

            
            var result = await _adminService.GetUserByIdAsync(DependencyProvider.RequestContext, 1);

            
            Assert.Equal(Common.Enums.Status.Unknown, result.Status);
            Assert.Contains(result.Messages, m => m.ErrorCode == ErrorCodes.ADMIN_GET_USER_BY_ID_EXCEPTION);
        }

        [Fact]
        public async Task UpdateUserAsync_Success()
        {
            
            var user = new User { Id = 1 };
            var requestModel = new UpdateUserRequestModel { UserId = 1 };
            var response = new GenericServiceResponse<bool> { Status = Common.Enums.Status.Unknown };

            _unitOfWork.Setup(uow => uow.Users.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(user);
            _unitOfWork.Setup(uow => uow.Users.Update(It.IsAny<User>())).Returns(true);
            _unitOfWork.Setup(uow => uow.CompleteAsync()).ReturnsAsync(true);

            
            var result = await _adminService.UpdateUserAsync(DependencyProvider.RequestContext, requestModel);

            
            Assert.Equal(Common.Enums.Status.Success, result.Status);
            Assert.True(result.Data);
        }

        [Fact]
        public async Task UpdateUserAsync_Failure()
        {
            
            var user = new User { Id = 1 };
            var requestModel = new UpdateUserRequestModel { UserId = 1 };
            var response = new GenericServiceResponse<bool> { Status = Common.Enums.Status.Unknown };

            _unitOfWork.Setup(uow => uow.Users.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((User)null);

            
            var result = await _adminService.UpdateUserAsync(DependencyProvider.RequestContext, requestModel);

            
            Assert.Equal(Common.Enums.Status.Failure, result.Status);
            Assert.Contains(result.Messages, m => m.ErrorCode == ErrorCodes.ADMIN_UPDATE_USER_FAILED);
        }

        [Fact]
        public async Task UpdateUserAsync_ExceptionThrown()
        {
            
            var requestModel = new UpdateUserRequestModel { UserId = 1 };
            var response = new GenericServiceResponse<bool> { Status = Common.Enums.Status.Unknown };

            _unitOfWork.Setup(uow => uow.Users.GetByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("Database error"));

            
            var result = await _adminService.UpdateUserAsync(DependencyProvider.RequestContext, requestModel);

            
            Assert.Equal(Common.Enums.Status.Unknown, result.Status);
            Assert.Contains(result.Messages, m => m.ErrorCode == ErrorCodes.ADMIN_UPDATE_USER_EXCEPTION);
        }

        [Fact]
        public async Task DeleteUser_Success()
        {
            
            var user = new User { Id = 1, IsAdmin = false };
            var response = new GenericServiceResponse<bool> { Status = Common.Enums.Status.Unknown };

            _unitOfWork.Setup(uow => uow.Users.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(user);
            _unitOfWork.Setup(uow => uow.Users.Delete(It.IsAny<User>())).Returns(true);
            _unitOfWork.Setup(uow => uow.CompleteAsync()).ReturnsAsync(true);

            
            var result = await _adminService.DeleteUser(DependencyProvider.RequestContext, 1);

            
            Assert.Equal(Common.Enums.Status.Success, result.Status);
            Assert.True(result.Data);
        }

        [Fact]
        public async Task DeleteUser_Failure()
        {
            
            var user = new User { Id = 1, IsAdmin = false };
            var response = new GenericServiceResponse<bool> { Status = Common.Enums.Status.Unknown };

            _unitOfWork.Setup(uow => uow.Users.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((User)null);

            
            var result = await _adminService.DeleteUser(DependencyProvider.RequestContext, 1);

            
            Assert.Equal(Common.Enums.Status.Failure, result.Status);
            Assert.Contains(result.Messages, m => m.ErrorCode == ErrorCodes.ADMIN_DELETE_USER_FAILED);
        }

        [Fact]
        public async Task DeleteUser_ExceptionThrown()
        {
            
            var response = new GenericServiceResponse<bool> { Status = Common.Enums.Status.Unknown };

            _unitOfWork.Setup(uow => uow.Users.GetByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("Database error"));

            
            var result = await _adminService.DeleteUser(DependencyProvider.RequestContext, 1);

            
            Assert.Equal(Common.Enums.Status.Unknown, result.Status);
            Assert.Contains(result.Messages, m => m.ErrorCode == ErrorCodes.ADMIN_DELETE_USER_EXCEPTION);
        }

        [Fact]
        public async Task GetLast5Transactions_Success()
        {
            
            var transactions = new List<Transaction>
            {
                new Transaction {},
                new Transaction {},
                new Transaction {},
                new Transaction {},
                new Transaction {}
            };

            _transactionUnitOfWork.Setup(tuow => tuow.Transactions.GetLastFiveTransactions()).ReturnsAsync(transactions);

            
            var result = await _adminService.GetLast5Transactions();

            
            Assert.Equal(Common.Enums.Status.Success, result.Status);
            Assert.NotNull(result.Data);
            Assert.Equal(5, result.Data.Count());
        }

        [Fact]
        public async Task GetLast5Transactions_Failure()
        {
            
            _transactionUnitOfWork.Setup(tuow => tuow.Transactions.GetLastFiveTransactions()).ReturnsAsync((IEnumerable<Transaction>)null);

            
            var result = await _adminService.GetLast5Transactions();

            
            Assert.Equal(Common.Enums.Status.Failure, result.Status);
            Assert.Contains(result.Messages, m => m.ErrorCode == ErrorCodes.ADMIN_GET_LAST_FIVE_TRANSACTION_FAILED);
        }

        [Fact]
        public async Task GetLast5Transactions_ExceptionThrown()
        {
            
            _transactionUnitOfWork.Setup(tuow => tuow.Transactions.GetLastFiveTransactions()).ThrowsAsync(new Exception("Database error"));

            
            var result = await _adminService.GetLast5Transactions();

            
            Assert.Equal(Common.Enums.Status.Unknown, result.Status);
            Assert.Contains(result.Messages, m => m.ErrorCode == ErrorCodes.ADMIN_GET_LAST_FIVE_TRANSACTION_EXCEPTION);
        }
    }
}

