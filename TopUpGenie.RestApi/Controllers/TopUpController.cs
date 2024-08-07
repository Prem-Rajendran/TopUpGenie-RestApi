using TopUpGenie.DataAccess.DataModel;

namespace TopUpGenie.RestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TopUpController : ControllerBase
{
    private readonly ITopUpService _topUpService;

    public TopUpController(ITopUpService topUpService)
    {
        _topUpService = topUpService;
    }

    [Authorize(Roles = "admin, user")]
    [HttpGet]
    [Route("TopUpOptions")]
    public async Task<IResponse<IEnumerable<TopUpOption>>> TopUpOptions()
    {
        return await _topUpService.ListTopUpOptions();
    }

    [Authorize(Roles = "admin, user")]
    [HttpPost]
    [Route("Transact")]
    public async Task<IResponse<bool>> InitiateTransact([FromBody] InitiateTransactionRequestModel model)
    {
        var context = HttpContext.GetRequestContext();
        return await _topUpService.TopUpTransaction(context, model);
    }
}

