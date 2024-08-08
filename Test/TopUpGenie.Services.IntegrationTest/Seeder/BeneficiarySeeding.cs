namespace TopUpGenie.Services.IntegrationTest.Seeder;

public class BeneficiarySeeding
{
    public static IEnumerable<Beneficiary> GetBeneficiaries()
    {
        var beneficiaries = new List<Beneficiary>();

        for (int i = 0; i < 3; i++)
        {
            beneficiaries.Add(new Beneficiary
            {
                Id = i+1,
                UserId = i+1,
                Nickname = $"XXX{i}",
                CreatedByUserId = 1,
                IsActive = true,
            });
        }

        return beneficiaries;
    }
}