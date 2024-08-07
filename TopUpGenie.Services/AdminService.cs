namespace TopUpGenie.Services;

/// <summary>
/// AdminService
/// </summary>
public class AdminService : IAdminService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionUnitOfWork _transactionUnitOfWork;

    public AdminService(IUnitOfWork unitOfWork, ITransactionUnitOfWork transactionUnitOfWork)
	{
        _unitOfWork = unitOfWork;
        _transactionUnitOfWork = transactionUnitOfWork;
    }

    /// <summary>
    /// CreateUserAsync
    /// </summary>
    /// <param name="requestContext"></param>
    /// <param name="requestModel"></param>
    /// <returns></returns>
    public async Task<IResponse<UserDto>> CreateUserAsync(RequestContext requestContext, CreateUserRequestModel requestModel)
    {
        GenericServiceResponse<UserDto> response = new() { Status = Common.Enums.Status.Unknown };
    
        try
        {
            User? user = requestModel.ToNewUser();

            if (user != null && await _unitOfWork.Users.AddAsync(user) && await _unitOfWork.CompleteAsync())
            {
                response.Status = Common.Enums.Status.Success;
                response.Data = new UserDto(user);
            }
            else
            {
                response.Status = Common.Enums.Status.Failure;
                response.AddMessage(ErrorCodes.ADMIN_CREATE_USER_FAILED, ErrorMessage.ADMIN_CREATE_USER_FAILED);
            }
        }
        catch (Exception ex)
        {
            response.AddMessage(ErrorCodes.ADMIN_CREATE_USER_EXCEPTION, ErrorMessage.ADMIN_CREATE_USER_EXCEPTION, ex);
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
            if (users != null && users.Any())
            {
                response.Status = Common.Enums.Status.Success;
                response.Data = users.Select(user => new UserDto(user));
            }
            else
            {
                response.Status = Common.Enums.Status.Failure;
                response.AddMessage(ErrorCodes.ADMIN_GET_ALL_USERS_FAILED, ErrorMessage.ADMIN_GET_ALL_USERS_FAILED);
            }
        }
        catch (Exception ex)
        {
            response.AddMessage(ErrorCodes.ADMIN_GET_ALL_USERS_EXCEPTION, ErrorMessage.ADMIN_GET_ALL_USERS_EXCEPTION, ex);
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
                response.Status = Common.Enums.Status.Success;
                response.Data = new UserDto(user);
            }
            else
            {
                response.Status = Common.Enums.Status.Failure;
                response.AddMessage(ErrorCodes.ADMIN_GET_USER_BY_ID_FAILED, ErrorMessage.ADMIN_GET_USER_BY_ID_FAILED);
            }
        }
        catch (Exception ex)
        {
            response.AddMessage(ErrorCodes.ADMIN_GET_USER_BY_ID_EXCEPTION, ErrorMessage.ADMIN_GET_USER_BY_ID_EXCEPTION, ex);
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
        GenericServiceResponse<bool> response = new() { Status = Common.Enums.Status.Unknown };

        try
        {
            User? user = await _unitOfWork.Users.GetByIdAsync(requestModel.UserId);
            user?.UpdateUser(requestModel, response);

            if (user != null && _unitOfWork.Users.Update(user) && await _unitOfWork.CompleteAsync())
            {
                response.Data = true;
                response.Status = Common.Enums.Status.Success;
            }
            else
            {
                response.Status = Common.Enums.Status.Failure;
                response.AddMessage(ErrorCodes.ADMIN_UPDATE_USER_FAILED, ErrorMessage.ADMIN_UPDATE_USER_FAILED);
            }
        }
        catch(Exception ex)
        {
            response.AddMessage(ErrorCodes.ADMIN_UPDATE_USER_EXCEPTION, ErrorMessage.ADMIN_UPDATE_USER_EXCEPTION, ex);
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
        GenericServiceResponse<bool> response = new() { Status = Common.Enums.Status.Unknown};

        try
        {
            User? user = await _unitOfWork.Users.GetByIdAsync(id);

            if (user != null && !user.IsAdmin && _unitOfWork.Users.Delete(user) && await _unitOfWork.CompleteAsync())
            {
                response.Status = Common.Enums.Status.Success;
                response.Data = true;
            }
            else
            {
                response.Status = Common.Enums.Status.Failure;
                response.AddMessage(ErrorCodes.ADMIN_DELETE_USER_FAILED, ErrorMessage.ADMIN_DELETE_USER_FAILED);
            }
        }
        catch (Exception ex)
        {
            response.AddMessage(ErrorCodes.ADMIN_DELETE_USER_EXCEPTION, ErrorMessage.ADMIN_DELETE_USER_EXCEPTION, ex);
        }

        return response;
    }

    /// <summary>
    /// GetLast5Transactions
    /// </summary>
    /// <returns></returns>
    public async Task<IResponse<IEnumerable<TransactionDto>>> GetLast5Transactions()
    {
        IResponse<IEnumerable<TransactionDto>> response = new GenericServiceResponse<IEnumerable<TransactionDto>> { Status = Common.Enums.Status.Unknown };
        try
        {
            IEnumerable<Transaction>? transactions = await _transactionUnitOfWork.Transactions.GetLastFiveTransactions();
            if (transactions != null && transactions.Any())
            {
                response.Status = Common.Enums.Status.Success;
                response.Data = transactions.Select(s => new TransactionDto(s));
            }
            else
            {
                response.Status = Common.Enums.Status.Failure;
                response.AddMessage(ErrorCodes.ADMIN_GET_LAST_FIVE_TRANSACTION_FAILED, ErrorMessage.ADMIN_GET_LAST_FIVE_TRANSACTION_FAILED);
            }
        }
        catch (Exception ex)
        {
            response.AddMessage(ErrorCodes.ADMIN_GET_LAST_FIVE_TRANSACTION_EXCEPTION, ErrorMessage.ADMIN_GET_LAST_FIVE_TRANSACTION_EXCEPTION, ex);
        }

        return response;
    }
}