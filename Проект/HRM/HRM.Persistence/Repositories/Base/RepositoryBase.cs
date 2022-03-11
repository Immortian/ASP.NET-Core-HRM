using Microsoft.EntityFrameworkCore;

namespace HRM.Persistence.Repositories.Base
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        internal readonly HRMDBContext context;

        public RepositoryBase(HRMDBContext context)
        {
            this.context = context;
        }
        public async Task<T> CreateAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            context.Set<T>().Update(entity);
            return entity;
        }
    }
}
