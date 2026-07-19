namespace TestApp.ToDoList.Application.Model
{
    /// <summary>
    /// Represents a request to edit a to-do item.
    /// </summary>
    /// <param name="Title">The title of the to-do item.</param>
    /// <param name="IsCompleted">Indicates whether the to-do item is completed.</param>
    public record EditToDoItem(string Title, bool IsCompleted)
    {
    }
}
