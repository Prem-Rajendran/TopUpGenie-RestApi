namespace TopUpGenie.Services;

public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly IUnitOfWork _unitOfWork;

    public TokenService(IOptions<JwtSettings> jwtSettings, IUnitOfWork unitOfWork)
    {
        _jwtSettings = jwtSettings.Value;
        _unitOfWork = unitOfWork;
    }

    public async Task<TokenResponseModel?> GenerateToken(IResponse<TokenResponseModel> response, User user, bool isAdmin)
	{
        TokenResponseModel? tokenResponse = null;

        try
        {
            var sessions = await _unitOfWork.Sessions.GetAllAsync();
            var existingSession = sessions.FirstOrDefault(s => s.UserId == user.Id);

            if (existingSession != null)
            {
                response.Messages ??= new List<Message>();
                response.Messages.Add(new Message()
                {
                    ErrorCode = ErrorCodes.TOKEN_SERVICE_EXISTING_SESSION,
                    Description = ErrorMessage.TOKEN_SERVICE_EXISTING_SESSION,
                });

                _unitOfWork.Sessions.Delete(existingSession);
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key ?? "");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name ?? ""),
                new Claim(ClaimTypes.Role, isAdmin ? "admin" : "user"),
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = Guid.NewGuid().ToString();

            tokenResponse = new TokenResponseModel
            {
                AccessToken = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = DateTime.Now.AddMinutes(5)
            };

            LoginSession? session =  tokenResponse.ToLoginSession(user);
            if (session != null && await _unitOfWork.Sessions.AddAsync(session))
                await _unitOfWork.CompleteAsync();
            else
            {
                tokenResponse = null;
                response.Messages.Add(new Message()
                {
                    ErrorCode = ErrorCodes.TOKEN_SERVICE_FAILED,
                    Description = ErrorMessage.TOKEN_SERVICE_FAILED,
                });
            }
        }
        catch (Exception ex)
        {
            response.Messages.Add(new Message()
            {
                ErrorCode = ErrorCodes.TOKEN_SERVICE_EXCEPTION,
                Description = string.Format(ErrorMessage.TOKEN_SERVICE_EXCEPTION, ex.Message),
            });
        }

        return tokenResponse;
    }
	}

