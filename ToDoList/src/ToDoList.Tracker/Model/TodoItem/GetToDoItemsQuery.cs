namespace TestApp.ToDoList.Application.Model.TodoItem
{
    /// <summary>
    /// Query object for retrieving to-do items with optional search term, pagination, and sorting.
    /// </summary>
    /// <param name="Filter"></param>
    /// <param name="Sorting"></param>    
    /// <param name="PageSize"></param>
    /// <param name="PageNumber"></param>
    public record GetToDoItemsQuery(
        GetToDoItemsQueryFilter Filter,
        GetToDoItemsQuerySorting Sorting,
        int? PageSize,
        int? PageNumber)
    {
    }

    /// <summary>
    /// Query object for specifying filtering criteria for retrieving to-do items.
    /// </summary>
    /// <param name="SearchTerm"></param>
    /// <param name="Tags"></param>
    /// <param name="Operand"></param>
    public record GetToDoItemsQueryFilter(string SearchTerm, string[] Tags, TagFilterOperand? Operand = TagFilterOperand.Any)
    {

    }

    /// <summary>
    /// Enum for specifying the operand to use when filtering to-do items by tags.
    /// </summary>
    public enum TagFilterOperand
    {
        /// <summary>
        /// Filter to-do items that contain any of the specified tags.
        /// </summary>
        Any,
        /// <summary>
        /// Filter to-do items that contain all of the specified tags.
        /// </summary>
        All
    }

    /// <summary>
    /// Query object for specifying sorting criteria for retrieving to-do items.
    /// </summary>
    /// <param name="SortBy"></param>
    /// <param name="DecendingOrder"></param>
    public record GetToDoItemsQuerySorting(
        SortBy? SortBy,
        bool DecendingOrder = false)
    {
    }

    /// <summary>
    /// Enum for specifying the sorting criteria for retrieving to-do items.
    /// </summary>
    public enum SortBy
    {
        /// <summary>
        /// Sort by the title of the to-do item.
        /// </summary>
        Title,
        /// <summary>
        /// Sort by the creation date of the to-do item.
        /// </summary>
        CreatedAt
    }
}
