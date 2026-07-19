using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.ToDoList.Domain.Entity;

namespace TestApp.ToDoList.Application.Common
{
    /// <summary>
    /// Repository interface for managing to-do items.
    /// </summary>
    public interface IToDoItemsRepository
    {
        /// <summary>
        /// Gets all to-do items from DB.
        /// </summary>
        /// <returns></returns>
        Task<ICollection<ToDoItem>> GetAllItemsAsync();
        /// <summary>
        /// Gets single to-do item by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ToDoItem> GetItemByIdAsync(int id);
        /// <summary>
        /// Creates a new to-do item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<ToDoItem> CreateAsync(ToDoItem item);
        /// <summary>
        /// Updates an existing to-do item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<ToDoItem> UpdateAsync(ToDoItem item);
        /// <summary>
        /// Delete an existing to-do item.
        /// </summary>
        /// <param name="id"></param>
        Task<ToDoItem> DeleteAsync(int id);
    }
}
