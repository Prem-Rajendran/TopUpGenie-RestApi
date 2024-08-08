namespace TopUpGenie.Services.UnitTest
{
	public class AuthServiceTests
	{
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<ITokenService> _tokenService;
        private IAuthService _authService;

        public AuthServiceTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _userRepository = new Mock<IUserRepository>();
            _tokenService = new Mock<ITokenService>();
            _unitOfWork.Setup(uow => uow.Users).Returns(_userRepository.Object);
            _authService = new AuthService(_unitOfWork.Object, _tokenService.Object);
            _unitOfWork.Setup(uow => uow.Users).Returns(_userRepository.Object);
        }



        [Fact]
        public async Task AuthenticateAsync_Success()
        {

            int userId = 1;
            var requestContext = DependencyProvider.RequestContext;
            requestContext.UserId = userId;

            var requestModel = new UserAuthenticationRequestModel { UserId = userId, Password = "password" };
            var user = new User { Id = userId, Password = PasswordHelper.GenerateHash("password") };
            var tokenResponse = new TokenResponseModel { AccessToken = "access_token", RefreshToken = "refresh_token" };

            _userRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);
            _tokenService.Setup(service => service.GenerateToken(It.IsAny<IResponse<TokenResponseModel>>(), user)).ReturnsAsync(tokenResponse);

            var response = await _authService.AuthenticateAsync(requestContext, requestModel);

            Assert.NotNull(response);
            Assert.Equal(Common.Enums.Status.Success, response.Status);
            Assert.NotNull(response.Data);
            Assert.Equal(tokenResponse, response.Data);
        }

        [Fact]
        public async Task AuthenticateAsync_InvalidPassword()
        {
            int userId = 1;
            var requestContext = DependencyProvider.RequestContext;
            requestContext.UserId = userId;

            var requestModel = new UserAuthenticationRequestModel { UserId = userId, Password = "wrong_password" };
            var user = new User { Id = userId, Password = PasswordHelper.GenerateHash("password") };

            _userRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);

            var response = await _authService.AuthenticateAsync(requestContext, requestModel);

            Assert.NotNull(response);
            Assert.Equal(Common.Enums.Status.Failure, response.Status);
            Assert.NotNull(response.Messages);
            Assert.Contains(response.Messages, m => m.ErrorCode == ErrorCodes.AUTHENTICATION_FAILED);
        }

        [Fact]
        public async Task AuthenticateAsync_UserNotFound()
        {
            int userId = 1;
            var requestContext = DependencyProvider.RequestContext;
            requestContext.UserId = userId;

            var requestModel = new UserAuthenticationRequestModel { UserId = userId, Password = "password" };

            _userRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync((User?)null);

            var response = await _authService.AuthenticateAsync(requestContext, requestModel);

      
            Assert.NotNull(response);
            Assert.Equal(Common.Enums.Status.Failure, response.Status);
            Assert.Contains(response.Messages, m => m.ErrorCode == ErrorCodes.AUTHENTICATION_FAILED);
        }

        [Fact]
        public async Task AuthenticateAsync_TokenGenerationFailed()
        {
            int userId = 1;
            var requestContext = DependencyProvider.RequestContext;
            requestContext.UserId = userId;

            var requestModel = new UserAuthenticationRequestModel { UserId = userId, Password = "password" };
            var user = new User { Id = userId, Password = PasswordHelper.GenerateHash("password") };

            _userRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);
            _tokenService.Setup(service => service.GenerateToken(It.IsAny<IResponse<TokenResponseModel>>(), user)).ReturnsAsync((TokenResponseModel?)null);

            var response = await _authService.AuthenticateAsync(requestContext, requestModel);

            Assert.NotNull(response);
            Assert.Equal(Common.Enums.Status.Failure, response.Status);
            Assert.Contains(response.Messages, m => m.ErrorCode == ErrorCodes.AUTHENTICATION_TOKEN_GENERATION_FAILED);
        }

        [Fact]
        public async Task AuthenticateAsync_ExceptionThrown()
        {
            int userId = 1;
            var requestContext = DependencyProvider.RequestContext;
            requestContext.UserId = userId;

            var requestModel = new UserAuthenticationRequestModel { UserId = userId, Password = "password" };

            _userRepository.Setup(repo => repo.GetByIdAsync(userId)).ThrowsAsync(new Exception("Database error"));

            var response = await _authService.AuthenticateAsync(requestContext, requestModel);

            Assert.NotNull(response);
            Assert.Equal(Common.Enums.Status.Unknown, response.Status);
            Assert.Contains(response.Messages, m => m.ErrorCode == ErrorCodes.AUTHENTICATION_EXCEPTION);
        }

        [Fact]
        public async Task InvalidateTokenAsync_Success()
        {
            int userId = 1;
            var requestContext = DependencyProvider.RequestContext;
            requestContext.UserId = userId;

            var user = new User { Id = userId };

            _userRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);
            _tokenService.Setup(service => service.InvalidateToken(It.IsAny<IResponse<bool>>(), user)).ReturnsAsync(true);

            var response = await _authService.InvalidateTokenAsync(requestContext);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task InvalidateTokenAsync_UserNotFound()
        {
            int userId = 1;
            var requestContext = DependencyProvider.RequestContext;
            requestContext.UserId = userId;

            _userRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync((User?)null);

            var response = await _authService.InvalidateTokenAsync(requestContext);

            Assert.NotNull(response);
            Assert.Equal(Common.Enums.Status.Failure, response.Status);
            Assert.Contains(response.Messages, m => m.ErrorCode == ErrorCodes.AUTHENTICATION_INVALIDATION_FAILED);
        }

        [Fact]
        public async Task InvalidateTokenAsync_TokenInvalidationFailed()
        {
            int userId = 1;
            var requestContext = DependencyProvider.RequestContext;
            requestContext.UserId = userId;

            var user = new User { Id = userId };

            _userRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);
            _tokenService.Setup(service => service.InvalidateToken(It.IsAny<IResponse<bool>>(), user)).ReturnsAsync(false);

            var response = await _authService.InvalidateTokenAsync(requestContext);

            Assert.NotNull(response);
            Assert.Equal(Common.Enums.Status.Failure, response.Status);
            Assert.Contains(response.Messages, m => m.ErrorCode == ErrorCodes.AUTHENTICATION_INVALIDATION_FAILED);
        }

        [Fact]
        public async Task InvalidateTokenAsync_ExceptionThrown()
        {
            int userId = 1;
            var requestContext = DependencyProvider.RequestContext;
            requestContext.UserId = userId;

            _userRepository.Setup(repo => repo.GetByIdAsync(userId)).ThrowsAsync(new Exception("Database error"));
            var response = await _authService.InvalidateTokenAsync(requestContext);

            
            Assert.NotNull(response);
            Assert.Equal(Common.Enums.Status.Unknown, response.Status);
            Assert.Contains(response.Messages, m => m.ErrorCode == ErrorCodes.AUTHENTICATION_EXCEPTION);
        }
    }
}

