namespace TestApp.ToDoList.Domain.Entity
{
    /// <summary>
    /// Represents a tag associated with a to-do item.
    /// </summary>
    public class ItemTag : EntityBase
    {
        /// <summary>
        /// Gets or sets the unique identifier of the to-do item associated with this tag.
        /// </summary>  
        public int ToDoItemId { get; set; }

        /// <summary>
        /// Gets or sets the value of the tag.
        /// </summary>
        public required string Value { get; set; }
    }
}
