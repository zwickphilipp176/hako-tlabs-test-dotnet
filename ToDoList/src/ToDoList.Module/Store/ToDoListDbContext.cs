using Microsoft.EntityFrameworkCore;
using TestApp.ToDoList.Domain.Entity;

namespace TestApp.ToDoList.Infrastructure.Store
{
    /// <summary>
    /// DbContext for the ToDo list.
    /// </summary>
    public class ToDoListDbContext : DbContext
    {
        /// <inheritdoc/>
        protected override void OnConfiguring
                (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseInMemoryDatabase(databaseName: "InMemoryTodoTasksDb");
        }

        /// <summary>
        /// DB Set for To-Do Items
        /// </summary>
        public DbSet<ToDoItem> ToDoItems { get; set; }

        /// <summary>
        /// DB Set for Item Tags
        /// </summary>
        public DbSet<ItemTag> ItemTags { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("NOCASE");
            
            base.OnModelCreating(modelBuilder);
            
            var entityModel = new ToDoListEntityModel();
            entityModel.ConfigureModel(modelBuilder);
        }
    }
}