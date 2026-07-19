using System.Threading.Tasks;
using TestApp.ToDoList.Domain.Entity;

namespace TestApp.ToDoList.Application.Common
{
    /// <summary>
    /// Repository interface for managing tags for to-do items.
    /// </summary>
    public interface IItemTagsRepository : IRepository<ItemTag>
    {
    }
}
