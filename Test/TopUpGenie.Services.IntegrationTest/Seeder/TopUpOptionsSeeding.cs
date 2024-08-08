namespace TopUpGenie.Services.IntegrationTest.Seeder;

public static class TopUpOptionsSeeding
{
	public static IEnumerable<TopUpOption> GetTopUpOptions()
	{
        List<TopUpOption> topUpOptions = new List<TopUpOption>()
        {
            new TopUpOption { TopUpOptionId = 1, Amount = 5, Description = "AED 5" },
            new TopUpOption { TopUpOptionId = 2, Amount = 10, Description = "AED 10" },
            new TopUpOption { TopUpOptionId = 3, Amount = 20, Description = "AED 20" },
            new TopUpOption { TopUpOptionId = 4, Amount = 30, Description = "AED 30" },
            new TopUpOption { TopUpOptionId = 5, Amount = 50, Description = "AED 50" },
            new TopUpOption { TopUpOptionId = 6, Amount = 75, Description = "AED 75" },
            new TopUpOption { TopUpOptionId = 7, Amount = 100, Description = "AED 100" }
        };

        return topUpOptions;
	}
}

