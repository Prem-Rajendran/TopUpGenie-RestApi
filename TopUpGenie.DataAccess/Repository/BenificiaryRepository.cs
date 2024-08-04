namespace TopUpGenie.DataAccess.Repository;

public class BenificiaryRepository : Repository<Beneficiary>, IBeneficiaryRepository
{
    private readonly TopUpGenieDbContext _context;
    private readonly ILogger<Repository<Beneficiary>> _logger;


    public BenificiaryRepository(TopUpGenieDbContext context, ILogger<Repository<Beneficiary>> logger) : base(context, logger)
    {
        _context = context;
        _logger = logger;
    }
}