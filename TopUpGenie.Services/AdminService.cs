using TopUpGenie.Common;
using TopUpGenie.Services.Constants;
using TopUpGenie.Services.Extensions;
using TopUpGenie.Services.Helper;
using TopUpGenie.Services.Models.Dto;

namespace TopUpGenie.Services;

public class AdminService : IAdminService
{
    private readonly IUnitOfWork _unitOfWork;

    public AdminService(IUnitOfWork unitOfWork)
	{
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// CreateUserAsync
    /// </summary>
    /// <param name="requestContext"></param>
    /// <param name="requestModel"></param>
    /// <returns></returns>
    public async Task<IResponse<CreateUserResponseModel>> CreateUserAsync(RequestContext requestContext, CreateUserRequestModel requestModel)
    {
        GenericServiceResponse<CreateUserResponseModel> response = new() { Status = Common.Enums.Status.Unknown };
    
        try
        {
            User user = requestModel.ToUser();
            Account account = requestModel.ToAccount();

            if (await _unitOfWork.CreateUserWithAccountAsync(user, account))
            {
                response.Status = Common.Enums.Status.Success;
                response.Data = new CreateUserResponseModel
                {
                    UserId = user.Id,
                    AccountNumber = account.AccountNumber
                };
            }
            else
            {
                response.Status = Common.Enums.Status.Failure;
                response.Messages = new List<Message>()
                {
                    new Message
                    {
                        ErrorCode = ErrorCodes.ADMIN_CREATE_USER_FAILED,
                        Description = ErrorMessage.ADMIN_CREATE_USER_FAILED
                    }
                };
            }
        }
        catch (Exception ex)
        {
            response.Messages = new List<Message>()
            {
                new Message
                {
                    ErrorCode = ErrorCodes.ADMIN_CREATE_USER_EXCEPTION,
                    Description = string.Format(ErrorMessage.ADMIN_CREATE_USER_EXCEPTION, ex.Message)
                }
            };
        }

        return response;
    }

    /// <summary>
    /// GetAllUsersAsync
    /// </summary>
    /// <param name="requestContext"></param>
    /// <returns></returns>
    public async Task<IResponse<IEnumerable<UserDto>>> GetAllUsersAsync(RequestContext requestContext)
    {
        GenericServiceResponse<IEnumerable<UserDto>> response = new() { Status = Common.Enums.Status.Unknown };

        try
        {
            IEnumerable<User> users = await _unitOfWork.Users.GetAllAsync();
            if (users != null && users.Count() > 0)
            {
                await _unitOfWork.CompleteAsync();
                response.Status = Common.Enums.Status.Success;
                response.Data = users.Select(user => new UserDto(user));
            }
            else
            {
                response.Status = Common.Enums.Status.Failure;
                response.Messages = new List<Message>
                {
                    new Message
                    {
                        ErrorCode = ErrorCodes.ADMIN_GET_ALL_USERS_FAILED,
                        Description = ErrorMessage.ADMIN_GET_ALL_USERS_FAILED
                    }
                };
            }
        }
        catch (Exception ex)
        {
            response.Messages = new List<Message>
            {
                new Message
                {
                    ErrorCode = ErrorCodes.ADMIN_GET_USER_BY_ID_EXCEPTION,
                    Description = string.Format(ErrorMessage.ADMIN_GET_USER_BY_ID_EXCEPTION, ex.Message)
                }
            };
        }

        return response;
    }

    /// <summary>
    /// GetUserByIdAsync
    /// </summary>
    /// <param name="requestContext"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IResponse<UserDto>> GetUserByIdAsync(RequestContext requestContext, int id)
    {
        GenericServiceResponse<UserDto> response = new() { Status = Common.Enums.Status.Unknown };

        try
        {
            User? user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user != null)
            {
                await _unitOfWork.CompleteAsync();
                response.Status = Common.Enums.Status.Success;
                response.Data = new UserDto(user);
            }
            else
            {
                response.Status = Common.Enums.Status.Failure;
                response.Messages = new List<Message>()
                {
                    new Message
                    {
                        ErrorCode = ErrorCodes.ADMIN_GET_USER_BY_ID_FAILED,
                        Description = ErrorMessage.ADMIN_GET_USER_BY_ID_FAILED
                    }
                };
            }
        }
        catch (Exception ex)
        {
            response.Messages = new List<Message>()
            {
                new Message
                {
                    ErrorCode = ErrorCodes.ADMIN_GET_USER_BY_ID_EXCEPTION,
                    Description = string.Format(ErrorMessage.ADMIN_GET_USER_BY_ID_EXCEPTION, ex.Message)
                }
            };
        }

        return response;
    }

    /// <summary>
    /// UpdateUserAsync
    /// </summary>
    /// <param name="requestContext"></param>
    /// <param name="requestModel"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<IResponse<bool>> UpdateUserAsync(RequestContext requestContext, UpdateUserRequestModel requestModel)
    {
        GenericServiceResponse<bool> response = new() { Status = Common.Enums.Status.Unknown, Messages = new List<Message>() };

        try
        {
            User? user = await _unitOfWork.Users.GetByIdAsync(requestModel.UserId);
            user?.UpdatePassword(requestModel, response);

            if (user != null && _unitOfWork.Users.Update(user))
            {
                await _unitOfWork.CompleteAsync();
                response.Data = true;
                response.Status = Common.Enums.Status.Success;
            }
            else
            {
                response.Status = Common.Enums.Status.Failure;
                response.Messages.Add(new Message
                {
                    ErrorCode = ErrorCodes.ADMIN_UPDATE_USER_FAILED,
                    Description = ErrorMessage.ADMIN_UPDATE_USER_FAILED
                });
            }
        }
        catch(Exception ex)
        {
            response.Messages.Add(new Message
            {
                ErrorCode = ErrorCodes.ADMIN_UPDATE_USER_EXCEPTION,
                Description = string.Format(ErrorMessage.ADMIN_UPDATE_USER_EXCEPTION, ex.Message)
            });
        }

        return response;
    }

    /// <summary>
    /// DeleteUser
    /// </summary>
    /// <param name="requestContext"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<IResponse<bool>> DeleteUser(RequestContext requestContext, int id)
    {
        GenericServiceResponse<bool> response = new() { Status = Common.Enums.Status.Unknown, Messages = new List<Message>() };

        try
        {
            var AdminTask = _unitOfWork.AdminUsers.GetByIdAsync(id);
            var UserTask = _unitOfWork.Users.GetByIdAsync(id);
            await Task.WhenAll(AdminTask, UserTask);

            Admin? admin = AdminTask.Result;
            User? user = UserTask.Result;

            if (admin == null && user != null && _unitOfWork.Users.Delete(user))
            {
                await _unitOfWork.CompleteAsync();
                response.Status = Common.Enums.Status.Success;
                response.Data = true;
            }
            else
            {
                response.Status = Common.Enums.Status.Failure;
                response.Messages.Add(new Message
                {
                    ErrorCode = ErrorCodes.ADMIN_DELETE_USER_FAILED,
                    Description = ErrorMessage.ADMIN_DELETE_USER_FAILED
                });
            }
        }
        catch (Exception ex)
        {
            response.Messages.Add(new Message
            {
                ErrorCode = ErrorCodes.ADMIN_DELETE_USER_EXCEPTION,
                Description = string.Format(ErrorMessage.ADMIN_DELETE_USER_EXCEPTION, ex.Message)
            });
        }

        return response;
    }
}

