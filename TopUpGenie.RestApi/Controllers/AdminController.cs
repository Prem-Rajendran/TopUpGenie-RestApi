using TopUpGenie.Common;
using TopUpGenie.Common.Interface;
using TopUpGenie.DataAccess.DataModel;
using TopUpGenie.Services.Helper;
using TopUpGenie.Services.Models.RequestModels;

namespace TopUpGenie.RestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{
    private TopUpGenieDbContext _dbContext;

    private readonly ILogger<AdminController> _logger;

    public AdminController(ILogger<AdminController> logger, TopUpGenieDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }


    [HttpPost]
    [Route("CreateUser")]
    public IResponse<User> CreateUser([FromBody] CreateUserRequestModel model)
    {
        GenericServiceResponse<User> response = new GenericServiceResponse<User>
        {
            Data = new User
            {
                Name = model.Name
            },
            Status = Common.Enums.Status.Success
        };
        return response;
    }
}

