namespace TopUpGenie.RestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private TopUpGenieDbContext _dbContext;

    private readonly ILogger<AuthenticationController> _logger;

    public AuthenticationController(ILogger<AuthenticationController> logger, TopUpGenieDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet(Name = "AuthenticationController")]
    public string Get()
    {
        return "AuthenticationController";
    }
}

