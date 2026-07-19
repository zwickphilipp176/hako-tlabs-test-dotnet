using Microsoft.EntityFrameworkCore;

using TestApp.ToDoList.Domain.Entity;

namespace TestApp.ToDoList.Infrastructure.Store
{
    /// <summary>ToDoList's EF model.</summary>
    public class ToDoListEntityModel
    {
        /// <inheritdoc/>
        public void ConfigureModel(ModelBuilder modBuilder)
        {
            modBuilder.Entity<ToDoItem>(s =>
            {
                s.HasKey(d => d.Id);
                s.HasIndex(d => d.Title);

                s.HasMany(d => d.Tags);
            });

            modBuilder.Entity<ItemTag>(s =>
            {
                s.HasKey(d => d.Id);
                s.HasIndex(d => d.Value);
            });
        }
    }
}