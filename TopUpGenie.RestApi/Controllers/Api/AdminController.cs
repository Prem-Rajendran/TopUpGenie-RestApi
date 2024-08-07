namespace TopUpGenie.RestApi.Controllers.Api;

/// <summary>
/// AdminController
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    /// <summary>
    /// GetUserById
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin")]
    [HttpGet]
    [Route("GetUserById")]
    public async Task<IResponse<UserDto>> GetUserById(int id)
    {
        var context = HttpContext.GetRequestContext();
        var response = await _adminService.GetUserByIdAsync(context, id);
        return response.ToApiResponse(HttpContext);
    }

    /// <summary>
    /// GetAllUsers
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "admin")]
    [HttpGet]
    [Route("GetAllUsers")]
    public async Task<IResponse<IEnumerable<UserDto>>> GetAllUsers()
    {
        var context = HttpContext.GetRequestContext();
        var response = await _adminService.GetAllUsersAsync(context);
        return response.ToApiResponse(HttpContext);
    }

    /// <summary>
    /// CreateUser
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin")]
    [HttpPost]
    [Route("CreateUser")]
    public async Task<IResponse<UserDto>> CreateUser([FromBody] CreateUserRequestModel model)
    {
        var context = HttpContext.GetRequestContext();
        var response = await _adminService.CreateUserAsync(context, model);
        return response.ToApiResponse(HttpContext);
    }

    /// <summary>
    /// UpdateUser
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin")]
    [HttpPut]
    [Route("UpdateUser")]
    public async Task<IResponse<bool>> UpdateUser([FromBody] UpdateUserRequestModel model)
    {
        var context = HttpContext.GetRequestContext();
        var response = await _adminService.UpdateUserAsync(context, model);
        return response.ToApiResponse(HttpContext);
    }

    /// <summary>
    /// DeleteUser
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin")]
    [HttpDelete]
    [Route("DeleteUser")]
    public async Task<IResponse<bool>> DeleteUser(int id)
    {
        var context = HttpContext.GetRequestContext();
        var response = await _adminService.DeleteUser(context, id);
        return response.ToApiResponse(HttpContext);
    }

    /// <summary>
    /// GetLast5Transaction
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "admin")]
    [HttpGet]
    [Route("GetLast5Transaction")]
    public async Task<IResponse<IEnumerable<TransactionDto>>> GetLast5Transaction()
    {
        var response = await _adminService.GetLast5Transactions();
        return response.ToApiResponse(HttpContext);
    }
}