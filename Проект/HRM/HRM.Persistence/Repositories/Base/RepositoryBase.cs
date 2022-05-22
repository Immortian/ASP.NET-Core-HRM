using HRM.Application.BuisnessLogic.Base;
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
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            context.Set<T>().Remove(entity);
            //await context.SaveChangesAsync();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public virtual async Task<T?> FirstAsync()
        {
            return await context.Set<T>().FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            if (context.Set<T>().Contains(entity))
            {
                context.Set<T>().Update(entity);
            }
            else
            {
                await context.Set<T>().AddAsync(entity);
            }
            //await context.SaveChangesAsync();
        }
    }
}
