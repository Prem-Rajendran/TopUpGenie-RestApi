namespace TopUpGenie.DataAccess.Interface;

public interface IRepository<T> where T : class
{
    /// <summary>
    /// GetByIdAsync
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// GetAllAsync
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// AddAsync
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> AddAsync(T entity);

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    bool Update(T entity);

    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    bool Delete(T entity);
}