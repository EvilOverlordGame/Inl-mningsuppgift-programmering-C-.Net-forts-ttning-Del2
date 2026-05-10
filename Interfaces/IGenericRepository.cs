using System.Linq.Expressions;
using bageri_api.Entities;

namespace bageri_api.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<T> FindByIdAsync(int id);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<T> FindAsync(Expression<Func<T, bool>> predicate);
    Task<bool> SaveAllAsync();
}
