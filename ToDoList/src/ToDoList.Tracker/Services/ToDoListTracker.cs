using System;
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
        private readonly IToDoItemsRepository todoItemsRepo;
        private readonly IItemTagsRepository itemTagsRepo;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="todoItemsRepo"></param>
        /// <param name="itemTagsRepo"></param>
        public ToDoListTracker(IToDoItemsRepository todoItemsRepo, IItemTagsRepository itemTagsRepo)
        {
            this.todoItemsRepo = todoItemsRepo;
            this.todoItemsRepo = todoItemsRepo;
        }

        /// <inheritdoc/>
        public async Task<ToDoItemDto> AddItemAsync(CreateToDoItem item)
        {
            // Implementation for adding a to-do item
            var newItem = new ToDoItem { Title = item.Title, IsCompleted = false, CreatedAt = TimeProvider.System.GetLocalNow().DateTime };
            newItem = await todoItemsRepo.CreateAsync(newItem);
            return ToDoItemDto.FromEntity(newItem);
        }

        /// <inheritdoc/>
        public async Task<ToDoItemDto> DeleteItemAsync(int id)
        {
            var deleted = await todoItemsRepo.DeleteAsync(id);
            if (deleted == null)
                throw new NotFoundException();

            return ToDoItemDto.FromEntity(deleted);
        }

        /// <inheritdoc/>
        public async Task<ToDoItemDto> GetItemAsync(int id)
        {
            // Implementation for getting a specific to-do item
            var item = await todoItemsRepo.GetItemByIdAsync(id);
            if (item == null)
                throw new NotFoundException();

            return ToDoItemDto.FromEntity(item);
        }

        /// <inheritdoc/>
        public async Task<PaginatedList<ToDoItemDto>> GetAllItemsAsync(GetToDoItemsQuery queryOptions)
        {
            // Implementation for getting all to-do items
            var (items, totalSize) = await todoItemsRepo.GetAllItemsAsync(queryOptions);
            return new PaginatedList<ToDoItemDto>(items.Select(ToDoItemDto.FromEntity).ToList(), totalSize);
        }

        /// <inheritdoc/>
        public async Task<ToDoItemDto> EditItemAsync(int id, EditToDoItem item)
        {
            var existingItem = await todoItemsRepo.GetItemByIdAsync(id);
            if (existingItem == null)
                throw new NotFoundException();

            existingItem.Title = item.Title;
            existingItem.IsCompleted = item.IsCompleted;
            existingItem.CompletedAt = item.IsCompleted ? DateTime.UtcNow : null;

            var updated = await todoItemsRepo.UpdateAsync(existingItem);
            return ToDoItemDto.FromEntity(updated);
        }

        /// <inheritdoc/>
        public async Task<ToDoItemDto> AddTagToItemAsync(int id, string tagValue)
        {
            var item = await todoItemsRepo.GetItemByIdAsync(id);
            if (item == null)
                throw new NotFoundException($"Todo item with Id {id} not found.");

            if(item.Tags.Any(x => x.Value.Equals(tagValue, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException($"Tag '{tagValue}' already exists for todo item with Id {id}.");

            item.Tags.Add(new ItemTag { Value = tagValue });
            var updatedItem = await todoItemsRepo.UpdateAsync(item);

            return ToDoItemDto.FromEntity(updatedItem);
        }

        /// <inheritdoc/>
        public async Task<ToDoItemDto> DeleteTagFromItemAsync(int id, string tagValue)
        {
            var item = await todoItemsRepo.GetItemByIdAsync(id);
            if (item == null)
                throw new NotFoundException();

            var tagToRemove = item.Tags.FirstOrDefault(x => x.Value.Equals(tagValue, StringComparison.OrdinalIgnoreCase));
            if (tagToRemove == null)
                throw new NotFoundException($"Tag '{tagValue}' not found for todo item with Id {id}.");

            item.Tags.Remove(tagToRemove);
            var updatedItem = await todoItemsRepo.UpdateAsync(item);

            return ToDoItemDto.FromEntity(updatedItem);
        }
    }
}