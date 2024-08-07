using System;
using TopUpGenie.DataAccess.DataModel;

namespace TopUpGenie.Services.Models.Dto
{
	public class BeneficiaryTransactionDto : BeneficiaryDto
	{
		public int TotalMonthlyTransaction { get; set; }

		public BeneficiaryTransactionDto(IEnumerable<Transaction>? transactions, Beneficiary beneficiary) : base(beneficiary)
		{
			if (transactions != null && transactions.Any())
			{
                TotalMonthlyTransaction = transactions.Where(t => t.BeneficiaryId == beneficiary.Id &&
                    t.TransactionStatus == "Success" &&
                    t.TransactionDate.Month == DateTime.Now.Month &&
                    t.TransactionDate.Year == DateTime.Now.Year)
                .Sum(t => t.TransactionAmount);
            }
        }
    }
}