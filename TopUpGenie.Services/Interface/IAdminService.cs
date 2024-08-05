using TopUpGenie.Services.Models.Dto;

namespace TopUpGenie.Services.Interface;

public interface IAdminService
{
	Task<IResponse<CreateUserResponseModel>> CreateUserAsync(RequestContext requestContext, CreateUserRequestModel requestModel);

	Task<IResponse<UserDto>> GetUserByIdAsync(RequestContext requestContext, int id);

	Task<IResponse<IEnumerable<UserDto>>> GetAllUsersAsync(RequestContext requestContext);

	Task<IResponse<bool>> UpdateUserAsync(RequestContext requestContext, UpdateUserRequestModel requestModel);

	Task<IResponse<bool>> DeleteUser(RequestContext requestContext, int id);
}