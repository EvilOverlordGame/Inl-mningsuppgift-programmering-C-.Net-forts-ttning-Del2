using System.Linq.Expressions;
using bageri_api.Data;
using bageri_api.Entities;
using bageri_api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bageri_api.Repositories;

public class GenericRepository<T>(BageriContext context) : IGenericRepository<T> where T : BaseEntity
{
    public void Add(T entity)
    {
        context.Set<T>().Add(entity);
    }

    public void Delete(T entity)
    {
        context.Set<T>().Remove(entity);
    }

    public Task<T> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return context.Set<T>().Where(predicate).FirstOrDefaultAsync();
    }

    public async Task<T> FindByIdAsync(int id)
    {
        return await context.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await context.Set<T>().ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void Update(T entity)
    {
        context.Entry(entity).State = EntityState.Modified;
    }
}
