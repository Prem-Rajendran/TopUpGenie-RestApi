using System;
namespace TopUpGenie.Services.Interface
{
	public interface ITransactionService
	{
		Task<bool> BeginTransact(User user, Beneficiary beneficiary, TopUpOption topUpOption);
	}
}

