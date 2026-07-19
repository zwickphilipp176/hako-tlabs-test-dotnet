using Microsoft.EntityFrameworkCore;
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
    public class ToDoItemsRepository : RepositoryBase<ToDoItem>, IToDoItemsRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToDoItemsRepository"/> class with the specified database context.
        /// </summary>
        /// <param name="context"></param>
        public ToDoItemsRepository(ToDoListDbContext context)
            : base(context, context.ToDoItems)
        {
            SeedInitialData();
        }

        private void SeedInitialData()
        {
            if (!context.ToDoItems.Any())
            {
                context.ToDoItems.AddRange(new[]
                {
                    new ToDoItem
                    {
                        Title = "Laundry",
                        Tags = [
                            new ItemTag { Value = "Household" }
                        ]
                    },
                    new ToDoItem 
                    { 
                        Title = "Grocery Shopping", 
                        IsCompleted = true,
                        Tags = [
                            new ItemTag { Value = "Finance" }                            
                        ]
                    },
                    new ToDoItem
                    {
                        Title = "Pay Bills",
                        Tags = [
                            new ItemTag { Value = "Finance" },
                            new ItemTag { Value = "Urgent" }
                        ]
                    },
                    new ToDoItem { Title = "Clean the House", IsCompleted = true}
                });
                context.SaveChanges();
            }
        }

        /// <inheritdoc/>
        public async Task<PaginatedList<ToDoItem>> GetAllItemsAsync(GetToDoItemsQuery queryOptions)
        {
            var query = context.ToDoItems.AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryOptions.Filter?.SearchTerm))
                query = query.Where(x => x.Title.Contains(queryOptions.Filter.SearchTerm));

            if (queryOptions.Filter?.Tags?.Any() == true)
            {
                if (queryOptions.Filter.Operand == TagFilterOperand.All)
                    query = query.Where(x => queryOptions.Filter.Tags.All(tag => x.Tags.Any(y => y.Value == tag)));
                if (queryOptions.Filter.Operand == TagFilterOperand.Any)
                    query = query.Where(x => x.Tags.Any(tag => queryOptions.Filter.Tags.Contains(tag.Value)));
            }

            var count = query.Count();

            if (queryOptions.Sorting != null)
            {
                query = queryOptions.Sorting.SortBy switch
                {
                    SortBy.Title => queryOptions.Sorting.DecendingOrder ? query.OrderByDescending(x => x.Title) : query.OrderBy(x => x.Title),
                    SortBy.CreatedAt => queryOptions.Sorting.DecendingOrder ? query.OrderByDescending(x => x.CreatedAt) : query.OrderBy(x => x.CreatedAt),
                    _ => query
                };
            }

            if (queryOptions.PageSize.HasValue && queryOptions.PageNumber.HasValue)
            {
                query = query.Skip((queryOptions.PageNumber.Value - 1) * queryOptions.PageSize.Value)
                             .Take(queryOptions.PageSize.Value);
            }

            return new PaginatedList<ToDoItem>(await query.ToListAsync(), count);
        }
    }
}