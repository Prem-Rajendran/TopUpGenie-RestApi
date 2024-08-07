namespace TopUpGenie.RestApi.Controllers.Api;

/// <summary>
/// TopUpController
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TopUpController : ControllerBase
{
    private readonly ITopUpService _topUpService;

    public TopUpController(ITopUpService topUpService)
    {
        _topUpService = topUpService;
    }

    /// <summary>
    /// TopUpOptions
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "admin, user")]
    [HttpGet]
    [Route("TopUpOptions")]
    public async Task<IResponse<IEnumerable<TopUpOption>>> TopUpOptions()
    {
        var response = await _topUpService.ListTopUpOptions();
        return response.ToApiResponse(HttpContext);
    }

    /// <summary>
    /// InitiateTransact
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin, user")]
    [HttpPost]
    [Route("Transact")]
    public async Task<IResponse<bool>> InitiateTransact([FromBody] InitiateTransactionRequestModel model)
    {
        var context = HttpContext.GetRequestContext();
        var response = await _topUpService.TopUpTransaction(context, model);
        return response.ToApiResponse(HttpContext);
    }
}

