namespace TopUpGenie.Services.Interface;

/// <summary>
/// IBeneficiaryService
/// </summary>
public interface IBeneficiaryService
{
    /// <summary>
    /// CreateBeneficiaryAsync
    /// </summary>
    /// <param name="requestContext"></param>
    /// <param name="requestModel"></param>
    /// <returns></returns>
    Task<IResponse<BeneficiaryDto>> CreateBeneficiaryAsync(RequestContext requestContext, CreateBeneficiaryRequestModel requestModel);

    /// <summary>
    /// GetMyBeneficiaryById
    /// </summary>
    /// <param name="requestContext"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IResponse<BeneficiaryDto>> GetMyBeneficiaryById(RequestContext requestContext, int id);

    /// <summary>
    /// ActivateMyBeneficiary
    /// </summary>
    /// <param name="requestContext"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IResponse<bool>> ActivateMyBeneficiary(RequestContext requestContext, int id);

    /// <summary>
    /// DeactivateMyBeneficiary
    /// </summary>
    /// <param name="requestContext"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IResponse<bool>> DeactivateMyBeneficiary(RequestContext requestContext, int id);

    /// <summary>
    /// GetMyBeneficiaries
    /// </summary>
    /// <param name="requestContext"></param>
    /// <returns></returns>
    Task<IResponse<IEnumerable<BeneficiaryDto>>> GetMyBeneficiaries(RequestContext requestContext);

    /// <summary>
    /// UpdateMyBeneficiary
    /// </summary>
    /// <param name="requestContext"></param>
    /// <param name="requestModel"></param>
    /// <param name="isActive"></param>
    /// <returns></returns>
    Task<IResponse<bool>> UpdateMyBeneficiary(RequestContext requestContext, UpdateBeneficiaryRequestModel requestModel, bool? isActive = null);

    /// <summary>
    /// DeleteMyBeneficiary
    /// </summary>
    /// <param name="requestContext"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IResponse<bool>> DeleteMyBeneficiary(RequestContext requestContext, int id);
}