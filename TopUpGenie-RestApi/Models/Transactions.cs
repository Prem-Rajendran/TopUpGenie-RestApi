using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopUpGenie_RestApi.Models
{
	public class Transactions
	{
		[Key]
		public int Id { get; set; }

		[ForeignKey("User")]
		public int UserId { get; set; }

		[ForeignKey("SourceAccount")]
		public int SourceAccountId { get; set; }

		[ForeignKey("DestinationAccount")]
		public int DestinationAccountId { get; set; }

		[ForeignKey("Beneficiary")]
		public int BeneficiaryId { get; set; }

		public int Amount { get; set; }

		public int TransactionFee { get; set; }

		public int TotalAmount { get; set; }

		public string? Currency { get; set; }

		public int BalanceBefore { get; set; }

		public int BalanceAfter { get; set; }

		public string? Description { get; set; }

		public DateTime TransactionDate { get; set; }


		public User? User { get; set; }

        public Account? SourceAccount { get; set; }

        public Account? DestinationAccount { get; set; }

		public Beneficiary? Beneficiary { get; set; }
    }
}

