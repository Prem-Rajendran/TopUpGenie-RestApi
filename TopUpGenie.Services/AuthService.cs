using System.Security.Authentication;
using TopUpGenie.Services.Constants;
using TopUpGenie.Services.Extensions;

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

    public async Task<IResponse<TokenResponseModel>> AuthenticateAsync(RequestContext requestContext, UserAuthenticationRequestModel requestModel)
    {
        GenericServiceResponse<TokenResponseModel> response = new GenericServiceResponse<TokenResponseModel> { Status = Common.Enums.Status.Unknown };

        try
        {
            var AdminTask = _unitOfWork.AdminUsers.GetByIdAsync(requestModel.UserId);
            var UserTask = _unitOfWork.Users.GetByIdAsync(requestModel.UserId);
            await Task.WhenAll(AdminTask, UserTask);

            Admin? admin = AdminTask.Result;
            User? user = UserTask.Result;

            if (user != null && PasswordHelper.VerifyPassword(user.Password, requestModel.Password))
            {
                TokenResponseModel? tokenResponse = await _tokenService.GenerateToken(response, user, admin != null);

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
}

