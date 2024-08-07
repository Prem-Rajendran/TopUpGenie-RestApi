namespace TopUpGenie.RestApi.Controllers.Api;

/// <summary>
/// BeneficiariesController
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BeneficiariesController : ControllerBase
{
    private readonly IBeneficiaryService _beneficiaryService;

    public BeneficiariesController(IBeneficiaryService beneficiaryService)
    {
        _beneficiaryService = beneficiaryService;
    }

    /// <summary>
    /// GetMyBeneficiaryById
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin, user")]
    [HttpGet]
    [Route("GetMyBeneficiaryById")]
    public async Task<IResponse<BeneficiaryDto>> GetMyBeneficiaryById(int id)
    {
        var context = HttpContext.GetRequestContext();
        var response = await _beneficiaryService.GetMyBeneficiaryById(context, id);
        return response.ToApiResponse(HttpContext);
    }

    /// <summary>
    /// GetMyBeneficiaries
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "admin, user")]
    [HttpGet]
    [Route("GetMyBeneficiaries")]
    public async Task<IResponse<IEnumerable<BeneficiaryDto>>> GetMyBeneficiaries()
    {
        var context = HttpContext.GetRequestContext();
        var response = await _beneficiaryService.GetMyBeneficiaries(context);
        return response.ToApiResponse(HttpContext);
    }

    /// <summary>
    /// CreateBeneficiaryAsync
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin, user")]
    [HttpPost]
    [Route("CreateBeneficiaryAsync")]
    public async Task<IResponse<BeneficiaryDto>> CreateBeneficiaryAsync([FromBody] CreateBeneficiaryRequestModel model)
    {
        var context = HttpContext.GetRequestContext();
        var response = await _beneficiaryService.CreateBeneficiaryAsync(context, model);
        return response.ToApiResponse(HttpContext);
    }

    /// <summary>
    /// ActivateMyBeneficiary
    /// </summary>
    /// <param name="beneficiaryId"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin, user")]
    [HttpPut]
    [Route("ActivateMyBeneficiary")]
    public async Task<IResponse<bool>> ActivateMyBeneficiary(int beneficiaryId)
    {
        var context = HttpContext.GetRequestContext();
        var response = await _beneficiaryService.ActivateMyBeneficiary(context, beneficiaryId);
        return response.ToApiResponse(HttpContext);
    }

    /// <summary>
    /// DeactivateMyBeneficiary
    /// </summary>
    /// <param name="beneficiaryId"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin, user")]
    [HttpPut]
    [Route("DeactivateMyBeneficiary")]
    public async Task<IResponse<bool>> DeactivateMyBeneficiary(int beneficiaryId)
    {
        var context = HttpContext.GetRequestContext();
        var response = await _beneficiaryService.DeactivateMyBeneficiary(context, beneficiaryId);
        return response.ToApiResponse(HttpContext);
    }

    /// <summary>
    /// UpdateMyBeneficiary
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin, user")]
    [HttpPut]
    [Route("UpdateMyBeneficiary")]
    public async Task<IResponse<bool>> UpdateMyBeneficiary([FromBody] UpdateBeneficiaryRequestModel model)
    {
        var context = HttpContext.GetRequestContext();
        var response = await _beneficiaryService.UpdateMyBeneficiary(context, model);
        return response.ToApiResponse(HttpContext);
    }

    /// <summary>
    /// DeleteMyBeneficiary
    /// </summary>
    /// <param name="beneficiaryId"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin, user")]
    [HttpPut]
    [Route("DeleteMyBeneficiary")]
    public async Task<IResponse<bool>> DeleteMyBeneficiary(int beneficiaryId)
    {
        var context = HttpContext.GetRequestContext();
        var response = await _beneficiaryService.DeleteMyBeneficiary(context, beneficiaryId);
        return response.ToApiResponse(HttpContext);
    }
}

