namespace TopUpGenie.DataAccess.IntegrationTest.Helper;

public static class TransactionSeeding
{
	public static IEnumerable<Transaction> GetTransaction()
	{
        List<Transaction> transactions = new List<Transaction>();

        for (int i = 0; i < 35; i++)
        {
            transactions.Add(new Transaction
            {
                UserId = 1,
                BeneficiaryId = 2,
                TopUpOptionId = 6,
                TransactionAmount = 75,
                TransactionFee = 1,
                TotalTransactionAmount = 76,
                TransactionStatus = i % 4 == 0 ? Enums.TransactionStatus.FAILED : Enums.TransactionStatus.SUCCESS,
                TransactionDate = DateTime.Now,
                Messages = "SomeMessage"
            });
        }

        return transactions;
    }
}

