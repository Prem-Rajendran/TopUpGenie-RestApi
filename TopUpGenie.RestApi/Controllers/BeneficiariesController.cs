namespace TopUpGenie.RestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BeneficiariesController : ControllerBase
{
    private TopUpGenieDbContext _dbContext;

    private readonly ILogger<BeneficiariesController> _logger;

    public BeneficiariesController(ILogger<BeneficiariesController> logger, TopUpGenieDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet(Name = "BeneficiariesController")]
    public string Get()
    {
        return "BeneficiariesController";
    }
}

