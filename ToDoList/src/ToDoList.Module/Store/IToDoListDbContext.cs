using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TestApp.ToDoList.Domain.Entity;

namespace TestApp.ToDoList.Infrastructure.Store
{
    /// <summary>
    /// Abstraction over the ToDoList database context, allowing repositories to depend on a
    /// contract rather than a concrete EF Core <see cref="DbContext"/> implementation.
    /// </summary>
    public interface IToDoListDbContext
    {
        /// <summary>
        /// DB Set for To-Do Items
        /// </summary>
        DbSet<ToDoItem> ToDoItems { get; }

        /// <summary>
        /// DB Set for Item Tags
        /// </summary>
        DbSet<ItemTag> ItemTags { get; }

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        int SaveChanges();

        /// <summary>
        /// Asynchronously saves all changes made in this context to the database.
        /// </summary>
        /// <param name="cancellationToken"></param>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
