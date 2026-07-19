using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TestApp.ToDoList.Application.Common;
using TestApp.ToDoList.Domain.Entity;
using TestApp.ToDoList.Infrastructure.Store;

namespace TestApp.ToDoList.Infrastructure.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RepositoryBase<T> : IRepository<T>
        where T: EntityBase
    {
        /// <summary>
        /// Context for the ToDoList database, used for database operations.
        /// </summary>
        protected readonly ToDoListDbContext context;

        /// <summary>
        /// DbSet for the entity type T, used for database operations.
        /// </summary>
        protected readonly DbSet<T> dbSet;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="dbSet"></param>
        public RepositoryBase(ToDoListDbContext context, DbSet<T> dbSet)
        {
            this.context = context;
            this.dbSet = dbSet;
        }

        /// <inheritdoc/>
        public async Task<T> CreateAsync(T item)
        {
            await dbSet.AddAsync(item);
            await context.SaveChangesAsync();
            return item;
        }

        /// <inheritdoc/>
        public async Task<T> DeleteAsync(int id)
        {
            var item = await dbSet.FindAsync(id);
            if (item != null)
            {
                dbSet.Remove(item);
                await context.SaveChangesAsync();
            }
            return item;
        }

        /// <inheritdoc/>
        public async Task<bool> ExistsAsync(int id)
        {
            return await dbSet.AnyAsync(x => x.Id == id);
        }

        /// <inheritdoc/>
        public async Task<T> GetItemByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task<T> UpdateAsync(T item)
        {
            dbSet.Update(item);
            await context.SaveChangesAsync();
            return item;
        }
    }
}
