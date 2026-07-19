using System;
using System.Threading.Tasks;
using TestApp.ToDoList.Application.Common;
using TestApp.ToDoList.Domain.Entity;
using TestApp.ToDoList.Infrastructure.Store;

namespace TestApp.ToDoList.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for managing tags for to-do items.
    /// </summary>
    public class ItemTagsRepository : RepositoryBase<ItemTag>, IItemTagsRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemTagsRepository"/> class.
        /// </summary>
        /// <param name="context"></param>
        public ItemTagsRepository(IToDoListDbContext context)
            :base(context, context.ItemTags)
        {

        }

    }
}
