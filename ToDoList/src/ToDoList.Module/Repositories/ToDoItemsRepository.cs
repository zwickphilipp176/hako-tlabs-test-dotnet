using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.ToDoList.Application.Common;
using TestApp.ToDoList.Application.Model;
using TestApp.ToDoList.Application.Model.TodoItem;
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
        public async Task<PaginatedList<ToDoItem>> GetAllItemsAsync(GetToDoItemsQuery queryOptions)
        {
            var query = context.ToDoItems.AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryOptions.SearchTerm))
                query = query.Where(x => x.Title.Contains(queryOptions.SearchTerm));

            var count = query.Count();

            query = queryOptions.SortBy switch
            {
                SortBy.Title => queryOptions.DecendingOrder ? query.OrderByDescending(x => x.Title) : query.OrderBy(x => x.Title),
                SortBy.CreatedAt => queryOptions.DecendingOrder ? query.OrderByDescending(x => x.CreatedAt) : query.OrderBy(x => x.CreatedAt),
                _ => query
            };

            if (queryOptions.PageSize.HasValue && queryOptions.PageNumber.HasValue)
            {
                query = query.Skip((queryOptions.PageNumber.Value - 1) * queryOptions.PageSize.Value)
                             .Take(queryOptions.PageSize.Value);
            }

            return new PaginatedList<ToDoItem>(await query.ToListAsync(), count);
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