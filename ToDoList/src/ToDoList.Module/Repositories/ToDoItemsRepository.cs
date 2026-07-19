using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.ToDoList.Application.Common;
using TestApp.ToDoList.Domain.Entity;
using TestApp.ToDoList.Infrastructure.Store;

namespace TestApp.ToDoList.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for managing to-do items.
    /// </summary>
    public class ToDoItemsRepository : IToDoItemsRepository
    {
        private readonly ToDoListDbContext context;

        /// <summary>
        /// Ctor. Seeds initial data.
        /// </summary>
        /// <param name="context"></param>
        public ToDoItemsRepository(ToDoListDbContext context)
        {
            this.context = context;

            if (!context.ToDoItems.Any())
            {
                context.ToDoItems.AddRange([
                    new ToDoItem { Title = "Laundry"},
                    new ToDoItem { Title = "Grocery Shopping", IsCompleted = true},
                    new ToDoItem { Title = "Pay Bills"},
                    new ToDoItem { Title = "Clean the House", IsCompleted = true}]);

               context.SaveChanges();
            }
        }

        /// <inheritdoc/>
        public async Task<ICollection<ToDoItem>> GetAllItemsAsync()
        {
            return await context.ToDoItems.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<ToDoItem> GetItemByIdAsync(int id)
        {
            return await context.ToDoItems.FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task<ToDoItem> CreateAsync(ToDoItem item)
        {
            await context.ToDoItems.AddAsync(item);
            await context.SaveChangesAsync();
            return item;
        }

        /// <inheritdoc/>
        public async Task<ToDoItem> UpdateAsync(ToDoItem item)
        {
            context.ToDoItems.Update(item);
            await context.SaveChangesAsync();
            return item;
        }

        /// <inheritdoc/>
        public async Task<ToDoItem> DeleteAsync(int id)
        {
            var item = await context.ToDoItems.FindAsync(id);
            if (item != null)
            {
                context.ToDoItems.Remove(item);
                await context.SaveChangesAsync();
            }
            return item;
        }

    }
}