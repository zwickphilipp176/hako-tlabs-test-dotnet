namespace TestApp.ToDoList.Domain.Entity
{
    /// <summary>
    /// Entity of the ToDo item
    /// </summary>
    public class ToDoItem : EntityBase
    {
        /// <summary>
        /// Title of the to-do item.
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// IsCompleted flag of the to-do item.
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Creation date of the to-do item.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Completion date of the to-do item.
        /// </summary>
        public DateTime? CompletedAt { get; set; }

        /// <summary>
        /// Collection of tags associated with the to-do item.
        /// </summary>
        public virtual ICollection<ItemTag> Tags { get; set; } = new List<ItemTag>();
    }
}