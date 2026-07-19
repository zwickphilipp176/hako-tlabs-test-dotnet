using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.ToDoList.Application.Common;
using TestApp.ToDoList.Application.Model;
using TestApp.ToDoList.Application.Model.TodoItem;
using TestApp.ToDoList.Domain.Entity;

namespace TestApp.ToDoList.Application.Services
{
    /// <summary>
    /// Implementation of the to-do list tracking.
    /// </summary>
    public class ToDoListTracker : IToDoListTracker
    {
        private readonly IToDoItemsRepository repository;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="repository"></param>
        public ToDoListTracker(IToDoItemsRepository repository)
        {
            this.repository = repository;
        }

        /// <inheritdoc/>
        public async Task<ToDoItem> AddItemAsync(CreateToDoItem item)
        {
            // Implementation for adding a to-do item
            var newItem = new ToDoItem { Title = item.Title, IsCompleted = false, CreatedAt = TimeProvider.System.GetLocalNow().DateTime };
            newItem = await repository.CreateAsync(newItem);
            return newItem;
        }

        /// <inheritdoc/>
        public async Task<ToDoItem> RemoveItemAsync(int id)
        {
            return await repository.DeleteAsync(id);
        }

        /// <inheritdoc/>
        public async Task<ToDoItem> GetItemAsync(int id)
        {
            // Implementation for getting a specific to-do item
            var item = await repository.GetItemByIdAsync(id);
            return item;
        }

        /// <inheritdoc/>
        public async Task<PaginatedList<ToDoItem>> GetAllItemsAsync(GetToDoItemsQuery queryOptions)
        {
            // Implementation for getting all to-do items
            return await repository.GetAllItemsAsync(queryOptions);
        }

        /// <inheritdoc/>
        public async Task<ToDoItem> EditItemAsync(int id, EditToDoItem item)
        {
            var existingItem = await repository.GetItemByIdAsync(id);

            if (null == existingItem)
                return null;

            existingItem.Title = item.Title;
            existingItem.IsCompleted = item.IsCompleted;
            existingItem.CompletedAt = item.IsCompleted ? DateTime.UtcNow : null;

            await repository.UpdateAsync(existingItem);
            return existingItem;
        }
    }
}