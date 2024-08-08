namespace TopUpGenie.Services.IntegrationTest.Seeder;

public static class TransactionSeeding
{
	public static IEnumerable<Transaction> GetTransaction()
	{
        List<Transaction> transactions = new List<Transaction>();

        for (int i = 0; i < 35; i++)
        {
            transactions.Add(new Transaction
            {
                Id = i+1,
                UserId = 1,
                BeneficiaryId = 2,
                TopUpOptionId = 6,
                TransactionAmount = 75,
                TransactionFee = 1,
                TotalTransactionAmount = 76,
                TransactionStatus = i % 5 == 0 ? DataAccess.Enums.TransactionStatus.FAILED : DataAccess.Enums.TransactionStatus.SUCCESS,
                TransactionDate = DateTime.Now,
                Messages = "SomeMessage"
            });
        }

        return transactions;
    }
}

