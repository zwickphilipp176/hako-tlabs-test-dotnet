using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.ToDoList.Application.Model.TodoItem;
using TestApp.ToDoList.Domain.Entity;

namespace TestApp.ToDoList.Application.Common
{
    /// <summary>
    /// Tracking to-do list items.
    /// </summary>
    public interface IToDoListTracker
    {
        /// <summary>
        /// Adds a new to-do item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<ToDoItem> AddItemAsync(CreateToDoItem item);

        /// <summary>
        /// Removes a to-do item.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the deleted item or null if none found</returns>
        Task<ToDoItem> RemoveItemAsync(int id);

        /// <summary>
        /// Gets a to-do item by its Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ToDoItem> GetItemAsync(int id);

        /// <summary>
        /// Gets all to-do items.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ToDoItem>> GetAllItemsAsync();

        /// <summary>
        /// Edits a to-do item.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<ToDoItem> EditItemAsync(int id, EditToDoItem item);
    }
}