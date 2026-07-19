using System;
using System.Linq;

namespace TestApp.ToDoList.Application.Model.TodoItem
{
    /// <summary>
    /// Dto for ToDoItem entity.
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Title"></param>
    /// <param name="IsCompleted"></param>
    /// <param name="CreatedAt"></param>
    /// <param name="CompletedAt"></param>
    /// <param name="Tags"></param>
    public record ToDoItemDto(int Id, string Title, bool IsCompleted, DateTime CreatedAt, DateTime? CompletedAt, string[] Tags)
    {
        /// <summary>
        /// Creates a ToDoItemDto from a ToDoItem entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ToDoItemDto FromEntity(Domain.Entity.ToDoItem entity)
        {
            return new ToDoItemDto(
                entity.Id,
                entity.Title,
                entity.IsCompleted,
                entity.CreatedAt,
                entity.CompletedAt,
                entity.Tags.Select(t => t.Value).ToArray()
            );
        }
    }
}
