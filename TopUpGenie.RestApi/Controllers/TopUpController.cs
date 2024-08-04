namespace TopUpGenie.RestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TopUpController : ControllerBase
{
    private TopUpGenieDbContext _dbContext;

    private readonly ILogger<TopUpController> _logger;

    public TopUpController(ILogger<TopUpController> logger, TopUpGenieDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet(Name = "TopUpController")]
    public string Get()
    {
        return "TopUpController";
    }
}

