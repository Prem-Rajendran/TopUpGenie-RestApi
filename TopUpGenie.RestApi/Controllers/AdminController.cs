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

    [HttpGet(Name = "AdminController")]
    public string Get()
    {
        return "AdminController";
    }
}

