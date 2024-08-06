namespace TopUpGenie.DataAccess.Repository;

public class TopUpOptionsRepository : Repository<TopUpOption>, ITopUpOptionsRepository
{
    public TopUpOptionsRepository(TopUpGenieDbContext context, ILogger<Repository<TopUpOption>> logger) : base(context, logger)
    {
    }
}
