namespace TopUpGenie.DataAccess.IntegrationTest.Helper;

public class BeneficiarySeeding
{
    public static IEnumerable<Beneficiary> GetBeneficiaries()
    {
        var beneficiaries = new List<Beneficiary>();

        for (int i = 0; i < 5; i++)
        {
            beneficiaries.Add(new Beneficiary
            {
                UserId = i+1,
                Nickname = $"XXX{i}",
                CreatedByUserId = 1,
                IsActive = true,
            });
        }

        return beneficiaries;
    }
}

