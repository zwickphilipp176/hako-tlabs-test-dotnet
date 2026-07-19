using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.ToDoList.Application.Model
{
    /// <summary>
    /// Represents a paginated list of items with a total count.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public record PaginatedList<T>(IReadOnlyList<T> Items, int TotalCount) where T : class
    {
    }
}
