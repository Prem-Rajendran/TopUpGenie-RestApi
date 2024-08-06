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

    public async Task<int> GetCountOfMyActiveBeneficiary(int userId)
    {
        IEnumerable<Beneficiary> beneficiaries = await _context.Beneficiaries.ToListAsync();
        return beneficiaries.Where(b => (b.CreatedByUserId == userId && b.IsActive == true)).Count();
    }

    public new async Task<IEnumerable<Beneficiary>> GetAllAsync()
    {

        return await _context.Beneficiaries
            .Include(b => b.BeneficiaryUser)
            .Include(b => b.CreatedByUser)
            .ToListAsync();
    }

    public new async Task<Beneficiary?> GetByIdAsync(int id)
    {
        return await _context.Beneficiaries
            .Include(b => b.BeneficiaryUser)
            .Include(b => b.CreatedByUser)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}