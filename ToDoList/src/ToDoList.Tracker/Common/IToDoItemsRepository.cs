using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.ToDoList.Application.Model;
using TestApp.ToDoList.Application.Model.TodoItem;
using TestApp.ToDoList.Domain.Entity;

namespace TestApp.ToDoList.Application.Common
{
    /// <summary>
    /// Repository interface for managing to-do items.
    /// </summary>
    public interface IToDoItemsRepository : IRepository<ToDoItem>
    {
        /// <summary>
        /// Gets all to-do items from DB filtered and sorted by the provided query options.
        /// </summary>
        /// <param name="queryOptions"></param>
        /// <returns></returns>
        Task<PaginatedList<ToDoItem>> GetAllItemsAsync(GetToDoItemsQuery queryOptions);
    }
}
