namespace TestApp.ToDoList.Application.Model
{
    /// <summary>
    /// Represents a request to create a new to-do item.
    /// </summary>
    /// <param name="Title">The title of the to-do item.</param>
    public record CreateToDoItem(string Title)
    {
    }
}
