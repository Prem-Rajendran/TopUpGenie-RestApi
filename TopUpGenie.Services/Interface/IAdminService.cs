namespace TopUpGenie.Services.Interface;

/// <summary>
/// IAdminService
/// </summary>
public interface IAdminService
{
    /// <summary>
    /// CreateUserAsync
    /// </summary>
    /// <param name="requestContext"></param>
    /// <param name="requestModel"></param>
    /// <returns></returns>
    Task<IResponse<UserDto>> CreateUserAsync(RequestContext requestContext, CreateUserRequestModel requestModel);

    /// <summary>
    /// GetUserByIdAsync
    /// </summary>
    /// <param name="requestContext"></param>
    /// <param name="id"></param>
    /// <returns></returns>
	Task<IResponse<UserDto>> GetUserByIdAsync(RequestContext requestContext, int id);

    /// <summary>
    /// GetAllUsersAsync
    /// </summary>
    /// <param name="requestContext"></param>
    /// <returns></returns>
	Task<IResponse<IEnumerable<UserDto>>> GetAllUsersAsync(RequestContext requestContext);

    /// <summary>
    /// GetLast5Transactions
    /// </summary>
    /// <returns></returns>
	Task<IResponse<IEnumerable<TransactionDto>>> GetLast5Transactions();

    /// <summary>
    /// UpdateUserAsync
    /// </summary>
    /// <param name="requestContext"></param>
    /// <param name="requestModel"></param>
    /// <returns></returns>
	Task<IResponse<bool>> UpdateUserAsync(RequestContext requestContext, UpdateUserRequestModel requestModel);

    /// <summary>
    /// DeleteUser
    /// </summary>
    /// <param name="requestContext"></param>
    /// <param name="id"></param>
    /// <returns></returns>
	Task<IResponse<bool>> DeleteUser(RequestContext requestContext, int id);
}