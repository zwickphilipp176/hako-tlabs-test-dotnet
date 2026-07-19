namespace TestApp.ToDoList.Application.Model.TodoItem
{
    /// <summary>
    /// Query object for retrieving to-do items with optional search term, pagination, and sorting.
    /// </summary>
    /// <param name="SearchTerm"></param>
    /// <param name="PageSize"></param>
    /// <param name="PageNumber"></param>
    /// <param name="SortBy"></param>
    /// <param name="DecendingOrder"></param>
    public record GetToDoItemsQuery(
        string SearchTerm,
        int? PageSize,
        int? PageNumber,
        SortBy? SortBy,
        bool DecendingOrder = false)
    {
    }

    /// <summary>
    /// Enum for specifying the sorting criteria for retrieving to-do items.
    /// </summary>
    public enum SortBy
    {        
        Title,
        CreatedAt
    }
}
