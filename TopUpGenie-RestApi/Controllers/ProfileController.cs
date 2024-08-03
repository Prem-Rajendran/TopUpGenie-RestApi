using Microsoft.AspNetCore.Mvc;
using TopUpGenie_RestApi.Data;

namespace TopUpGenie_RestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfileController : ControllerBase
{
    private TopUpGenieDbContext _dbContext;

    private readonly ILogger<ProfileController> _logger;

    public ProfileController(ILogger<ProfileController> logger, TopUpGenieDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet(Name = "ProfileController")]
    public string Get()
    {
        return "ProfileController";
    }
}

