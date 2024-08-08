using Microsoft.Extensions.Options;
using TopUpGenie.Common;
using TopUpGenie.Services.Settings;

namespace TopUpGenie.Services.UnitTest
{
    public class TokenServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<ISessionRepository> _sessionRepository;
        private readonly IOptions<JwtSettings> _jwtSettings;
        private readonly ITokenService _tokenService;

        public TokenServiceTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _sessionRepository = new Mock<ISessionRepository>();
            _jwtSettings = Options.Create(new JwtSettings {
                Key = "A1b2C3d4E5f6G7h8I9j0K1l2M3n4O5p6Q7r8S9t0U1v2W3x4Y5z6A7b8C9d0E1f2",
                Issuer = "TopUpGenieIssuer",
                Audience = "TopUpGenieAudience",
                ExpireMinutes = 60
            });

            _unitOfWork.Setup(uow => uow.Sessions).Returns(_sessionRepository.Object);

            _tokenService = new TokenService(_jwtSettings, _unitOfWork.Object);
        }

        [Fact]
        public async Task GenerateToken_Success()
        {
            
            var user = new User { Id = 1, Password = "hashed_password", LoginSessions = new List<LoginSession>() };
            var response = new GenericServiceResponse<TokenResponseModel> { Status = Common.Enums.Status.Unknown };

            _sessionRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync((IEnumerable<LoginSession>)null);

            
            var tokenResponse = await _tokenService.GenerateToken(response, user);

            
            Assert.NotNull(tokenResponse);
            _sessionRepository.Verify(repo => repo.AddAsync(It.IsAny<LoginSession>()), Times.Once);
            _unitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task GenerateToken_ExistingSessionExpired()
        {
            
            var user = new User { Id = 1, Password = "hashed_password", LoginSessions = new List<LoginSession>() };
            var expiredSession = new LoginSession { UserId = user.Id, ExpirationDateTime = DateTime.Now.AddDays(-1) };
            var response = new GenericServiceResponse<TokenResponseModel> { Status = Common.Enums.Status.Unknown };

            _sessionRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<LoginSession> { expiredSession });

            
            var tokenResponse = await _tokenService.GenerateToken(response, user);

            
            Assert.NotNull(tokenResponse);
            _sessionRepository.Verify(repo => repo.Delete(expiredSession), Times.Once);
            _sessionRepository.Verify(repo => repo.AddAsync(It.IsAny<LoginSession>()), Times.Once);
        }

        [Fact]
        public async Task GenerateToken_ExistingSessionValid()
        {
            
            var user = new User { Id = 1, Password = "hashed_password", LoginSessions = new List<LoginSession>() };
            var validSession = new LoginSession { UserId = user.Id, ExpirationDateTime = DateTime.Now.AddDays(1) };
            var response = new GenericServiceResponse<TokenResponseModel> { Status = Common.Enums.Status.Unknown };

            _sessionRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<LoginSession> { validSession });

            
            var tokenResponse = await _tokenService.GenerateToken(response, user);

            
            Assert.NotNull(tokenResponse);
            Assert.Contains(response.Messages, m => m.ErrorCode == ErrorCodes.TOKEN_SERVICE_EXISTING_SESSION);
        }

        [Fact]
        public async Task GenerateToken_ExceptionThrown()
        {
            
            var user = new User { Id = 1, Password = "hashed_password", LoginSessions = new List<LoginSession>() };
            var response = new GenericServiceResponse<TokenResponseModel> { Status = Common.Enums.Status.Unknown };

            _sessionRepository.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new Exception("Database error"));

            
            var tokenResponse = await _tokenService.GenerateToken(response, user);

            
            Assert.Null(tokenResponse);
            Assert.Equal(Common.Enums.Status.Unknown, response.Status);
            Assert.Contains(response.Messages, m => m.ErrorCode == ErrorCodes.TOKEN_SERVICE_EXCEPTION);
        }

        [Fact]
        public async Task InvalidateToken_Success()
        {
            
            var user = new User { Id = 1, Password = "hashed_password" };
            var session = new LoginSession { UserId = user.Id };
            var response = new GenericServiceResponse<bool> { Status = Common.Enums.Status.Unknown };

            _sessionRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<LoginSession> { session });

            
            var result = await _tokenService.InvalidateToken(response, user);

            
            Assert.True(result);
            Assert.Equal(Common.Enums.Status.Success, response.Status);
            _sessionRepository.Verify(repo => repo.Delete(session), Times.Once);
            _unitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task InvalidateToken_SessionNotFound()
        {
            
            var user = new User { Id = 1, Password = "hashed_password" };
            var response = new GenericServiceResponse<bool> { Status = Common.Enums.Status.Unknown };

            _sessionRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<LoginSession>());

            
            var result = await _tokenService.InvalidateToken(response, user);

            
            Assert.False(result);
            Assert.Equal(Common.Enums.Status.Failure, response.Status);
            Assert.Contains(response.Messages, m => m.ErrorCode == ErrorCodes.TOKEN_INVALIDATION_FAILED);
        }

        [Fact]
        public async Task InvalidateToken_ExceptionThrown()
        {
            
            var user = new User { Id = 1, Password = "hashed_password" };
            var response = new GenericServiceResponse<bool> { Status = Common.Enums.Status.Unknown };

            _sessionRepository.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new Exception("Database error"));

            
            var result = await _tokenService.InvalidateToken(response, user);

            
            Assert.False(result);
            Assert.Equal(Common.Enums.Status.Unknown, response.Status);
            Assert.Contains(response.Messages, m => m.ErrorCode == ErrorCodes.TOKEN_INVALIDATION_EXCEPTION);
        }
    }
}

