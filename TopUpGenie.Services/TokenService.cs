namespace TopUpGenie.Services;

/// <summary>
/// TokenService
/// </summary>
public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly IUnitOfWork _unitOfWork;

    public TokenService(IOptions<JwtSettings> jwtSettings, IUnitOfWork unitOfWork)
    {
        _jwtSettings = jwtSettings.Value;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// GenerateToken
    /// </summary>
    /// <param name="response"></param>
    /// <param name="user"></param>
    /// <param name="isAdmin"></param>
    /// <returns></returns>
    public async Task<TokenResponseModel?> GenerateToken(IResponse<TokenResponseModel> response, User user)
	{
        TokenResponseModel? tokenResponse = null;

        try
        {
            var existingSession = user?.LoginSessions?.FirstOrDefault();
            existingSession  ??= (await _unitOfWork.Sessions.GetAllAsync())?.FirstOrDefault(e => e.UserId == user?.Id);

            if (existingSession != null)
            {
                response.AddMessage(ErrorCodes.TOKEN_SERVICE_EXISTING_SESSION, ErrorMessage.TOKEN_SERVICE_EXISTING_SESSION);

                if (existingSession.ExpirationDateTime < DateTime.Now)
                {
                    _unitOfWork.Sessions.Delete(existingSession);
                    await _unitOfWork.CompleteAsync();
                    existingSession = null;
                }
            }

            tokenResponse = GetTokenResponse(user);

            LoginSession? session =  tokenResponse.ToLoginSession(user);
            if (session != null)
            {
                if (existingSession == null)
                    await _unitOfWork.Sessions.AddAsync(session);
                else
                    _unitOfWork.Sessions.Update(session);
            }
            else
            {
                tokenResponse = null;
                response.AddMessage(ErrorCodes.TOKEN_SERVICE_FAILED, ErrorMessage.TOKEN_SERVICE_FAILED);
            }

            await _unitOfWork.CompleteAsync();
        }
        catch (Exception ex)
        {
            response.AddMessage(ErrorCodes.TOKEN_SERVICE_EXCEPTION, ErrorMessage.TOKEN_SERVICE_EXCEPTION, ex);
        }

        return tokenResponse;
    }

    /// <summary>
    /// InvalidateToken
    /// </summary>
    /// <param name="response"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<bool> InvalidateToken(IResponse<bool> response, User user)
    {
        try
        {
            var sessions = await _unitOfWork.Sessions.GetAllAsync();
            var currentSession = sessions.FirstOrDefault(s => s.UserId == user.Id);

            if (currentSession != null)
            {
                response.Status = Common.Enums.Status.Success;
                response.Data = _unitOfWork.Sessions.Delete(currentSession);
                await _unitOfWork.CompleteAsync();

                return true;
            }
            else
            {
                response.Status = Common.Enums.Status.Failure;
                response.AddMessage(ErrorCodes.TOKEN_INVALIDATION_FAILED, ErrorMessage.TOKEN_INVALIDATION_FAILED);
            }
        }
        catch (Exception ex)
        {
            response.AddMessage(ErrorCodes.TOKEN_INVALIDATION_EXCEPTION, ErrorMessage.TOKEN_INVALIDATION_EXCEPTION, ex);
        }

        return false;
    }

    /// <summary>
    /// ValidateToken
    /// </summary>
    /// <param name="response"></param>
    /// <param name="requestContext"></param>
    /// <returns></returns>
    public async Task<IResponse<ValidateTokenRequestModel>?> ValidateToken(IResponse<ValidateTokenRequestModel> response, string accessToken)
    {
        try
        {
            SecurityToken validatedToken = await ValidatedToken(response, accessToken);

            if (response != null && response.Status == Common.Enums.Status.Success && response.Data != null && validatedToken is JwtSecurityToken jwtToken && jwtToken.ValidTo < DateTime.UtcNow)
            {
                var result = await RefreshToken(response);
                if (result != null)
                    response.Data.TokenResponse = result;
                else
                    response.AddMessage(ErrorCodes.TOKEN_REFRESH_FAILED, ErrorMessage.TOKEN_REFRESH_FAILED);
            }

            return response;
        }
        catch (Exception ex)
        {
            response.AddMessage(ErrorCodes.TOKEN_VALIDATION_EXCEPTION, ErrorMessage.TOKEN_VALIDATION_EXCEPTION, ex);
        }

        return response;
    }


    #region Private Methods

    private async Task<SecurityToken> ValidatedToken(IResponse<ValidateTokenRequestModel> response, string accessToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Key ?? "");

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = _jwtSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = _jwtSettings.Audience,
            RequireExpirationTime = true,
            ValidateLifetime = false,
            ClockSkew = TimeSpan.Zero
        };

        var principle = tokenHandler.ValidateToken(accessToken, validationParameters, out SecurityToken validatedToken);
        response.Data = new ValidateTokenRequestModel();

        // Check additional validation if needed
        if (!(validatedToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            response.Status = Common.Enums.Status.Failure;
            response.AddMessage(ErrorCodes.TOKEN_VALIDATION_FAILED, ErrorMessage.TOKEN_VALIDATION_FAILED);
        }
        else
        {
            _ = int.TryParse(principle.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value, out int id);
            var sessions = await _unitOfWork.Sessions.GetAllAsync();
            var currentSession = sessions.FirstOrDefault(e => e.UserId == id);

            if (currentSession != null && currentSession.AccessToken == accessToken)
            {
                response.Status = Common.Enums.Status.Success;
                response.Data.claimsPrincipal = principle;
                response.Data.UserId = id;
                response.Data.TokenResponse = new TokenResponseModel
                {
                    AccessToken = currentSession.AccessToken,
                    RefreshToken = currentSession.RefreshToken,
                    Expiration = currentSession.ExpirationDateTime
                };
            }
            else
            {
                response.Status = Common.Enums.Status.Failure;
                response.AddMessage(ErrorCodes.TOKEN_VALIDATION_FAILED, ErrorMessage.TOKEN_VALIDATION_FAILED);
            }
        }

        return validatedToken;
    }

    private async Task<TokenResponseModel?> RefreshToken(IResponse<ValidateTokenRequestModel> response)
    {
        User? user = await _unitOfWork.Users.GetByIdAsync(response.Data.UserId);
        
        if (user != null)
        {
            var tokenResponse = new GenericServiceResponse<TokenResponseModel>();
            var res = await GenerateToken(tokenResponse, user);
            response.Messages ??= new List<Message>();
            tokenResponse.Messages?.ForEach(response.Messages.Add);

            return res;
        }

        response.Status = Common.Enums.Status.Failure;
        return null;
    }

    private TokenResponseModel GetTokenResponse(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Key ?? "");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name ?? ""),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "admin" : "user"),
            }),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            Expires = DateTime.Now.AddMinutes(_jwtSettings.ExpireMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var refreshToken = Guid.NewGuid().ToString();

        return new TokenResponseModel
        {
            AccessToken = tokenHandler.WriteToken(token),
            RefreshToken = refreshToken,
            Expiration = DateTime.Now.AddHours(1)
        };
    }

    #endregion
}