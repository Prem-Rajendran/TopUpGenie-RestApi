namespace TopUpGenie.DataAccess.Repository;

/// <summary>
/// Repository
/// </summary>
/// <typeparam name="T"></typeparam>
public class Repository<T> : IRepository<T> where T : class
{
    private readonly TopUpGenieDbContext _context;
    private readonly DbSet<T> _dbSet;
    private readonly ILogger<Repository<T>> _logger;


    public Repository(TopUpGenieDbContext context, ILogger<Repository<T>> logger)
    {
        _context = context;
        _dbSet = _context.Set<T>();
        _logger = logger;
    }

    /// <summary>
    /// AddAsync
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<bool> AddAsync(T entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Add Async Failed", ex);
        }

        return false;
    }

    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public bool Delete(T entity)
    {
        try
        {
            _dbSet.Remove(entity);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Delete Failed", ex);

        }
        return false;
    }

    /// <summary>
    /// GetAllAsync
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        try
        {
            return await _dbSet.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("Get All Async Failed", ex);
        }

        return new List<T>();
    }

    /// <summary>
    /// GetByIdAsync
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<T?> GetByIdAsync(int id)
    {
        try
        {
            T? entity = await _dbSet.FindAsync(id);
            return entity ?? null;
        }
        catch (Exception ex)
        {
            _logger.LogError("Get By Id Async Failed", ex);
        }

        return null;
    }

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public bool Update(T entity)
    {
        try
        {
            _dbSet.Update(entity);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Update Failed", ex);
        }

        return false;
    }
}

