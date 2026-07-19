using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.ToDoList.Application.Common
{
    /// <summary>
    /// Base Repository interface for managing entities.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Gets a boolean indicating whether an entity with the specified Id exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(int id);

        /// <summary>
        /// Gets single entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetItemByIdAsync(int id);

        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<T> CreateAsync(T item);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<T> UpdateAsync(T item);

        /// <summary>
        /// Delete an existing entity.
        /// </summary>
        /// <param name="id"></param>
        Task<T> DeleteAsync(int id);
    }
}
