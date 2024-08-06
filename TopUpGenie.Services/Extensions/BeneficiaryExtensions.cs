
namespace TopUpGenie.Services.Extensions;

public static class BeneficiaryExtensions
{
	public static async Task<Beneficiary?> CreateNewBeneficiary(this CreateBeneficiaryRequestModel requestModel, IUnitOfWork unitOfWork, int createdByUserId)
	{
		if (requestModel != null && unitOfWork != null)
		{
			User? beneficiaryUser = await unitOfWork.Users.GetUserByPhoneNumber(requestModel.BeneficiaryPhoneNumber ?? "");

			if (beneficiaryUser != null)
			{
				return new Beneficiary
				{
					UserId = beneficiaryUser.Id,
					CreatedByUserId = createdByUserId,
					Nickname = requestModel.BeneficiaryNickname,
					IsActive = true,
					CreatedAt = DateTime.Now,
					UpdatedAt = DateTime.Now
                };

            }
        }
		return null;
	}

	public static async Task UpdateBeneficiary(this Beneficiary beneficiary, UpdateBeneficiaryRequestModel requestModel, IUnitOfWork unitOfWork, bool? isActive)
	{
		beneficiary.Id = requestModel.BeneficiaryId;

		if (!string.IsNullOrWhiteSpace(requestModel.BeneficiaryNickname))
			beneficiary.Nickname = requestModel.BeneficiaryNickname;

		if (!string.IsNullOrWhiteSpace(requestModel.BeneficiaryPhoneNumber))
		{
			User? user = await unitOfWork.Users.GetUserByPhoneNumber(requestModel.BeneficiaryPhoneNumber);
			if (user != null)
			{
				beneficiary.BeneficiaryUser = user;
				beneficiary.UserId = user.Id;
			}
		}

        if (isActive.HasValue)
			beneficiary.IsActive = isActive.Value;

		beneficiary.UpdatedAt = DateTime.Now;
    }
}

