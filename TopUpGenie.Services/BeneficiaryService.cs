using TopUpGenie.Common;
using TopUpGenie.Common.Interface;

namespace TopUpGenie.Services;

public class BeneficiaryService : IBeneficiaryService
{
    private readonly IUnitOfWork _unitOfWork;

    public BeneficiaryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// ActivateMyBeneficiary
    /// </summary>
    /// <param name="requestContext"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IResponse<bool>> ActivateMyBeneficiary(RequestContext requestContext, int id)
    {
        IResponse<bool> response = new GenericServiceResponse<bool> { Status = Common.Enums.Status.Success };
        UpdateBeneficiaryRequestModel requestModel = new() { BeneficiaryId = id };
        try
        {
            int count = await _unitOfWork.Beneficiaries.GetCountOfMyActiveBeneficiary(requestContext.UserId);
            if (count < 5)
            {
                return await UpdateMyBeneficiary(requestContext, requestModel, true);
            }
            else
            {
                response.Status = Common.Enums.Status.Failure;
                response.Messages = new List<Message>
                {
                    new Message
                    {
                        ErrorCode = "",
                        Description = ""
                    }
                };
            }
        }
        catch (Exception ex)
        {
            response.Messages = new List<Message>
            {
                new Message
                {
                    ErrorCode = "",
                    Description = ""
                }
            };
        }

        return response;
    }

    /// <summary>
    /// CreateBeneficiaryAsync
    /// </summary>
    /// <param name="requestContext"></param>
    /// <param name="requestModel"></param>
    /// <returns></returns>
    public async Task<IResponse<BeneficiaryDto>> CreateBeneficiaryAsync(RequestContext requestContext, CreateBeneficiaryRequestModel requestModel)
    {
        IResponse<BeneficiaryDto> response = new GenericServiceResponse<BeneficiaryDto> { Status = Common.Enums.Status.Unknown };

        try
        {
            int count = await _unitOfWork.Beneficiaries.GetCountOfMyActiveBeneficiary(requestContext.UserId);

            if (count < 5)
            {
                Beneficiary? beneficiary = await requestModel.CreateNewBeneficiary(_unitOfWork, requestContext.UserId);
                if (beneficiary != null && await _unitOfWork.Beneficiaries.AddAsync(beneficiary) && await _unitOfWork.CompleteAsync())
                {
                    response.Status = Common.Enums.Status.Success;
                    response.Data = new BeneficiaryDto(beneficiary);
                }
                else
                {
                    response.Status = Common.Enums.Status.Failure;
                    response.Messages = new List<Message>
                    {
                        new Message
                        {
                            ErrorCode = "",
                            Description = ""
                        }
                    };
                }
            }
            else
            {
                response.Status = Common.Enums.Status.Failure;
                response.Messages = new List<Message>
                {
                    new Message
                    {
                        ErrorCode = "",
                        Description = ""
                    }
                };
            }
        }
        catch (Exception ex)
        {
            response.Messages = new List<Message>
            {
                new Message
                {
                    ErrorCode = "",
                    Description = ""
                }
            };
        }

        return response;
    }

    /// <summary>
    /// DeactivateMyBeneficiary
    /// </summary>
    /// <param name="requestContext"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IResponse<bool>> DeactivateMyBeneficiary(RequestContext requestContext, int id)
    {
        UpdateBeneficiaryRequestModel requestModel = new() { BeneficiaryId = id };
        return await UpdateMyBeneficiary(requestContext, requestModel, false);
    }

    /// <summary>
    /// DeleteMyBeneficiary
    /// </summary>
    /// <param name="requestContext"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<IResponse<bool>> DeleteMyBeneficiary(RequestContext requestContext, int id)
    {
        IResponse<bool> response = new GenericServiceResponse<bool>();

        try
        {
            Beneficiary? beneficiary = await _unitOfWork.Beneficiaries.GetByIdAsync(id);
            if (beneficiary != null &&
                beneficiary.CreatedByUserId == requestContext.UserId &&
                _unitOfWork.Beneficiaries.Delete(beneficiary) &&
                await _unitOfWork.CompleteAsync())
            {
                response.Status = Common.Enums.Status.Success;
                response.Data = true;
            }
            else
            {
                response.Status = Common.Enums.Status.Failure;
                response.Messages = new List<Message>
                {
                    new Message
                    {
                        ErrorCode = "",
                        Description = ""
                    }
                };
            }
        }
        catch (Exception ex)
        {
            response.Messages = new List<Message>
            {
                new Message
                {
                    ErrorCode = "",
                    Description = ""
                }
            };
        }

        return response;
    }

    /// <summary>
    /// GetMyBeneficiaries
    /// </summary>
    /// <param name="requestContext"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<IResponse<IEnumerable<BeneficiaryDto>>> GetMyBeneficiaries(RequestContext requestContext)
    {
        IResponse<IEnumerable<BeneficiaryDto>> response = new GenericServiceResponse<IEnumerable<BeneficiaryDto>> { Status = Common.Enums.Status.Success };

        try
        {
            IEnumerable<Beneficiary> beneficiaries = await _unitOfWork.Beneficiaries.GetAllAsync();
            if (beneficiaries != null)
            {
                response.Status = Common.Enums.Status.Success;
                response.Data = beneficiaries.Where(b => b.CreatedByUserId == requestContext.UserId).Select(b => new BeneficiaryDto(b));
            }
            else
            {
                response.Status = Common.Enums.Status.Failure;
                response.Messages = new List<Message>
                {
                    new Message
                    {
                        ErrorCode = "",
                        Description = ""
                    }
                };
            }

        }
        catch (Exception ex)
        {
            response.Messages = new List<Message>
            {
                new Message
                {
                    ErrorCode = "",
                    Description = ""
                }
            };
        }

        return response;
    }

    /// <summary>
    /// GetMyBeneficiaryById
    /// </summary>
    /// <param name="requestContext"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<IResponse<BeneficiaryDto>> GetMyBeneficiaryById(RequestContext requestContext, int id)
    {
        IResponse<BeneficiaryDto> response = new GenericServiceResponse<BeneficiaryDto> { Status = Common.Enums.Status.Success };

        try
        {
            Beneficiary? beneficiary = await _unitOfWork.Beneficiaries.GetByIdAsync(id);
            if (beneficiary != null && beneficiary.CreatedByUserId == requestContext.UserId)
            {
                response.Status = Common.Enums.Status.Success;
                response.Data = new BeneficiaryDto(beneficiary);
            }
            else
            {
                response.Status = Common.Enums.Status.Failure;
                response.Messages = new List<Message>
                {
                    new Message
                    {
                        ErrorCode = "",
                        Description = ""
                    }
                };
            }

        }
        catch (Exception ex)
        {
            response.Messages = new List<Message>
            {
                new Message
                {
                    ErrorCode = "",
                    Description = ""
                }
            };
        }

        return response;
    }

    /// <summary>
    /// UpdateMyBeneficiary
    /// </summary>
    /// <param name="requestContext"></param>
    /// <param name="requestModel"></param>
    /// <param name="isActive"></param>
    /// <returns></returns>
    public async Task<IResponse<bool>> UpdateMyBeneficiary(RequestContext requestContext, UpdateBeneficiaryRequestModel requestModel, bool? isActive = null)
    {
        IResponse<bool> response = new GenericServiceResponse<bool> { Status = Common.Enums.Status.Unknown };
        try
        {
            Beneficiary? beneficiary = await _unitOfWork.Beneficiaries.GetByIdAsync(requestModel.BeneficiaryId);
            if (beneficiary != null && beneficiary.CreatedByUserId == requestContext.UserId)
            {
                await beneficiary.UpdateBeneficiary(requestModel, _unitOfWork, isActive);
                if (_unitOfWork.Beneficiaries.Update(beneficiary) && await _unitOfWork.CompleteAsync())
                {
                    response.Status = Common.Enums.Status.Success;
                    response.Data = true;
                }
            }
            else
            {
                response.Status = Common.Enums.Status.Failure;
                response.Messages = new List<Message>
                {
                    new Message
                    {
                        ErrorCode = "",
                        Description = ""
                    }
                };
            }
        }
        catch (Exception ex)
        {
            response.Messages = new List<Message>
                {
                    new Message
                    {
                        ErrorCode = "",
                        Description = ""
                    }
                };
        }

        return response;
    }
}