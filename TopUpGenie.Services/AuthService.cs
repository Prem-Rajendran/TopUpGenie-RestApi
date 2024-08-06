namespace TopUpGenie.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;

    public AuthService(IUnitOfWork unitOfWork, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }

    /// <summary>
    /// AuthenticateAsync
    /// </summary>
    /// <param name="requestContext"></param>
    /// <param name="requestModel"></param>
    /// <returns></returns>
    public async Task<IResponse<TokenResponseModel>> AuthenticateAsync(RequestContext requestContext, UserAuthenticationRequestModel requestModel)
    {
        GenericServiceResponse<TokenResponseModel> response = new GenericServiceResponse<TokenResponseModel> { Status = Common.Enums.Status.Unknown };

        try
        {
            User? user = await _unitOfWork.Users.GetByIdAsync(requestModel.UserId);

            if (user != null && PasswordHelper.VerifyPassword(user.Password, requestModel.Password))
            {
                TokenResponseModel? tokenResponse = await _tokenService.GenerateToken(response, user);

                if (tokenResponse != null)
                {
                    response.Status = Common.Enums.Status.Success;
                    response.Data = tokenResponse;
                }
                else
                {
                    response.Status = Common.Enums.Status.Failure;
                }
            }
            else
            {
                response.Status = Common.Enums.Status.Failure;
                response.Messages ??= new List<Message>();
                response.Messages.Add(new Message
                {
                    ErrorCode = ErrorCodes.AUTHENTICATION_FAILED,
                    Description = ErrorMessage.AUTHENTICATION_FAILED,
                });
            }
        }
        catch (Exception ex)
        {
            response.Messages ??= new List<Message>();
            response.Messages.Add(new Message
            {
                ErrorCode = ErrorCodes.AUTHENTICATION_EXCEPTION,
                Description = string.Format(ErrorMessage.AUTHENTICATION_EXCEPTION, ex.Message),
            });
        }

        return response;
    }


    /// <summary>
    /// InvalidateTokenAsync
    /// </summary>
    /// <param name="requestContext"></param>
    /// <returns></returns>
    public async Task<IResponse<bool>> InvalidateTokenAsync(RequestContext requestContext)
    {
        IResponse<bool> response = new GenericServiceResponse<bool> { Status = Common.Enums.Status.Unknown };

        try
        {
            User? user = await _unitOfWork.Users.GetByIdAsync(requestContext.UserId);
            if (user != null && await _tokenService.InvalidateToken(response, user))
                return response;
            else
            {
                response.Status = Common.Enums.Status.Failure;
                response.Messages ??= new List<Message>();
                response.Messages.Add(new Message
                {
                    ErrorCode = ErrorCodes.AUTHENTICATION_INVALIDATION_FAILED,
                    Description = ErrorMessage.AUTHENTICATION_INVALIDATION_FAILED
                });
            }
        }
        catch (Exception ex)
        {
            response.Messages ??= new List<Message>();
            response.Messages.Add(new Message
            {
                ErrorCode = ErrorCodes.AUTHENTICATION_INVALIDATION_EXCEPTION,
                Description = string.Format(ErrorMessage.AUTHENTICATION_INVALIDATION_EXCEPTION, ex.Message)
            });
        }

        return response;
    }
}

