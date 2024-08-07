namespace TopUpGenie.RestApi.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class BeneficiariesController : ControllerBase
{
    private readonly IBeneficiaryService _beneficiaryService;

    public BeneficiariesController(IBeneficiaryService beneficiaryService)
    {
        _beneficiaryService = beneficiaryService;
    }

    [Authorize(Roles = "admin, user")]
    [HttpGet]
    [Route("GetMyBeneficiaryById")]
    public async Task<IResponse<BeneficiaryDto>> GetMyBeneficiaryById(int id)
    {
        var context = HttpContext.GetRequestContext();
        return await _beneficiaryService.GetMyBeneficiaryById(context, id);
    }

    [Authorize(Roles = "admin, user")]
    [HttpGet]
    [Route("GetMyBeneficiaries")]
    public async Task<IResponse<IEnumerable<BeneficiaryDto>>> GetMyBeneficiaries()
    {
        var context = HttpContext.GetRequestContext();
        return await _beneficiaryService.GetMyBeneficiaries(context);
    }

    [Authorize(Roles = "admin, user")]
    [HttpPost]
    [Route("CreateBeneficiaryAsync")]
    public async Task<IResponse<BeneficiaryDto>> CreateBeneficiaryAsync([FromBody] CreateBeneficiaryRequestModel model)
    {
        var context = HttpContext.GetRequestContext();
        return await _beneficiaryService.CreateBeneficiaryAsync(context, model);
    }

    [Authorize(Roles = "admin, user")]
    [HttpPut]
    [Route("ActivateMyBeneficiary")]
    public async Task<IResponse<bool>> ActivateMyBeneficiary(int beneficiaryId)
    {
        var context = HttpContext.GetRequestContext();
        return await _beneficiaryService.ActivateMyBeneficiary(context, beneficiaryId);
    }

    [Authorize(Roles = "admin, user")]
    [HttpPut]
    [Route("DeactivateMyBeneficiary")]
    public async Task<IResponse<bool>> DeactivateMyBeneficiary(int beneficiaryId)
    {
        var context = HttpContext.GetRequestContext();
        return await _beneficiaryService.DeactivateMyBeneficiary(context, beneficiaryId);
    }

    [Authorize(Roles = "admin, user")]
    [HttpPut]
    [Route("UpdateMyBeneficiary")]
    public async Task<IResponse<bool>> UpdateMyBeneficiary([FromBody] UpdateBeneficiaryRequestModel model)
    {
        var context = HttpContext.GetRequestContext();
        return await _beneficiaryService.UpdateMyBeneficiary(context, model);
    }

    [Authorize(Roles = "admin, user")]
    [HttpPut]
    [Route("DeleteMyBeneficiary")]
    public async Task<IResponse<bool>> DeleteMyBeneficiary(int beneficiaryId)
    {
        var context = HttpContext.GetRequestContext();
        return await _beneficiaryService.DeleteMyBeneficiary(context, beneficiaryId);
    }
}

