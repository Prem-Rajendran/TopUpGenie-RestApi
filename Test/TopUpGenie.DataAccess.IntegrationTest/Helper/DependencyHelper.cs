namespace TopUpGenie.DataAccess.IntegrationTest.Helper;

public static class DependencyHelper
{
    public static TopUpGenieDbContext TopUpGenieDbContext => GetTopUpGenieDbContext();
	public static TransactionDbContext TransactionDbContext => GetTransactionDbContext();
    public static User ValidUser => GetDummyValidUser();
    public static User InValidUser => GetDummyInValidUser();
    public static Transaction ValidTransaction => GetValidTransaction();

    private static TopUpGenieDbContext GetTopUpGenieDbContext()
	{
        var dbContextFixture = new TopUpGenieDbContextFixture();
        var context = dbContextFixture.DbContext;

        if (!context.Users.Any())
        {
            context.Users.AddRange(UserSeeding.GetUsers());
            context.SaveChanges();
        }

        if (!context.Beneficiaries.Any())
        {
            context.Beneficiaries.AddRange(BeneficiarySeeding.GetBeneficiaries());
            context.SaveChanges();
        }

        if (!context.LoginSessions.Any())
        {
            context.LoginSessions.AddRange(LoginSessionSeeding.GetLoginSessions());
            context.SaveChanges();
        }

        if (!context.TopUpOptions.Any())
        {
            context.TopUpOptions.AddRange(TopUpOptionsSeeding.GetTopUpOptions());
            context.SaveChanges();
        }

        return context;
    }

	private static TransactionDbContext GetTransactionDbContext()
	{
        var dbContextFixture = new TransactionDbContextFixture();
        var context = dbContextFixture.DbContext;

        if (!context.Users.Any())
        {
            context.Users.AddRange(UserSeeding.GetUsers());
            context.SaveChanges();
        }

        if (!context.Beneficiaries.Any())
        {
            context.Beneficiaries.AddRange(BeneficiarySeeding.GetBeneficiaries());
            context.SaveChanges();
        }

        if (!context.TopUpOptions.Any())
        {
            context.TopUpOptions.AddRange(TopUpOptionsSeeding.GetTopUpOptions());
            context.SaveChanges();
        }

        if (!context.Transactions.Any())
        {
            context.Transactions.AddRange(TransactionSeeding.GetTransaction());
            context.SaveChanges();
        }

        return context;
    }

    private static Transaction GetValidTransaction()
    {
        return new Transaction
        {
            UserId = 1,
            BeneficiaryId = 2,
            TopUpOptionId = 6,
            TransactionAmount = 75,
            TransactionFee = 1,
            TotalTransactionAmount = 76,
            TransactionStatus = Enums.TransactionStatus.SUCCESS,
            TransactionDate = DateTime.Now,
            Messages = "SomeMessage"
        };
    }

    private static User GetDummyValidUser()
	{
		return new User
        {
            Name = "xxxx",
            Password = "password123",
            PhoneNumber = "1111111",
            Verified = true,
            IsAdmin = false,
            Balance = 1000,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    private static User GetDummyInValidUser()
    {
        return new User
        {
            Name = "xxxx"
        };
    }
}

