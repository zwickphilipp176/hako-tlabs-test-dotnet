using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.ToDoList.Application.Model;
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
        Task<ToDoItemDto> AddItemAsync(CreateToDoItem item);

        /// <summary>
        /// Removes a to-do item.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the deleted item or null if none found</returns>
        Task<ToDoItemDto> DeleteItemAsync(int id);

        /// <summary>
        /// Gets a to-do item by its Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ToDoItemDto> GetItemAsync(int id);

        /// <summary>
        /// Gets all to-do items.
        /// </summary>
        /// <param name="queryOptions"></param>
        /// <returns></returns>
        Task<PaginatedList<ToDoItemDto>> GetAllItemsAsync(GetToDoItemsQuery queryOptions);

        /// <summary>
        /// Edits a to-do item.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<ToDoItemDto> EditItemAsync(int id, EditToDoItem item);

        /// <summary>
        /// Adds a tag to a to-do item.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tagValue"></param>
        /// <returns></returns>
        Task<ToDoItemDto> AddTagToItemAsync(int id, string tagValue);

        /// <summary>
        /// Deletes a tag from a to-do item.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tagValue"></param>
        /// <returns></returns>
        Task<ToDoItemDto> DeleteTagFromItemAsync(int id, string tagValue);
    }
}