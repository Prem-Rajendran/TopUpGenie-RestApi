namespace TopUpGenie.Services.Interface
{
	public interface IBeneficiaryService
	{
        Task<IResponse<BeneficiaryDto>> CreateBeneficiaryAsync(RequestContext requestContext, CreateBeneficiaryRequestModel requestModel);

        Task<IResponse<BeneficiaryDto>> GetMyBeneficiaryById(RequestContext requestContext, int id);

        Task<IResponse<bool>> ActivateMyBeneficiary(RequestContext requestContext, int id);

        Task<IResponse<bool>> DeactivateMyBeneficiary(RequestContext requestContext, int id);

        Task<IResponse<IEnumerable<BeneficiaryDto>>> GetMyBeneficiaries(RequestContext requestContext);

        Task<IResponse<bool>> UpdateMyBeneficiary(RequestContext requestContext, UpdateBeneficiaryRequestModel requestModel, bool? isActive = null);

        Task<IResponse<bool>> DeleteMyBeneficiary(RequestContext requestContext, int id);
    }
}