using Microsoft.AspNetCore.Authorization;
using System.Data;
using TopUpGenie.Services.Models.Dto;

namespace TopUpGenie.RestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [Authorize(Roles = "admin")]
    [HttpGet]
    [Route("GetUserById")]
    public async Task<IResponse<UserDto>> GetUserById(int id)
    {
        var context = HttpContext.GetRequestContext();
        return await _adminService.GetUserByIdAsync(context, id);
    }

    [Authorize(Roles = "admin")]
    [HttpGet]
    [Route("GetAllUsers")]
    public async Task<IResponse<IEnumerable<UserDto>>> GetAllUsers()
    {
        var context = HttpContext.GetRequestContext();
        return await _adminService.GetAllUsersAsync(context);
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    [Route("CreateUser")]
    public async Task<IResponse<CreateUserResponseModel>> CreateUser([FromBody] CreateUserRequestModel model)
    {
        var context = HttpContext.GetRequestContext();
        return await _adminService.CreateUserAsync(context, model);
    }

    [Authorize(Roles = "admin")]
    [HttpPut]
    [Route("UpdateUser")]
    public async Task<IResponse<bool>> UpdateUser([FromBody] UpdateUserRequestModel model)
    {
        var context = HttpContext.GetRequestContext();
        return await _adminService.UpdateUserAsync(context, model);
    }

    [Authorize(Roles = "admin")]
    [HttpDelete]
    [Route("DeleteUser")]
    public async Task<IResponse<bool>> DeleteUser(int id)
    {
        var context = HttpContext.GetRequestContext();
        return await _adminService.DeleteUser(context, id);
    }
}