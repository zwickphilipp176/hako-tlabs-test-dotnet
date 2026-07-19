using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TestApp.ToDoList.Domain.Entity;
using TestApp.ToDoList.Infrastructure.Store;

namespace ToDoList.Application.Tests.Store
{
    /// <summary>
    /// Dedicated EF Core in-memory database context for functional tests, implementing
    /// <see cref="IToDoListDbContext"/>.
    /// </summary>
    public class TestToDoListDbContext : DbContext, IToDoListDbContext
    {
        private readonly string databaseName;

        /// <summary>
        /// Creates a new test database context backed by a uniquely named in-memory database.
        /// </summary>
        public TestToDoListDbContext()
        {
            this.databaseName = Guid.NewGuid().ToString();
        }

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseInMemoryDatabase(databaseName);
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

        /// <summary>
        /// Seeds the in-memory database with deterministic test data, unless it has already been seeded.
        /// </summary>
        public void SeedTestData()
        {
            if (ToDoItems.Any())
                return;

            ToDoItems.AddRange(new[]
            {
                new ToDoItem
                {
                    Title = "Write unit tests",
                    Tags = [ new ItemTag { Value = "Testing" } ]
                },
                new ToDoItem
                {
                    Title = "Review pull request",
                    IsCompleted = true,
                    Tags = [ new ItemTag { Value = "Work" } ]
                },
                new ToDoItem
                {
                    Title = "Deploy release",
                    Tags = [
                        new ItemTag { Value = "Work" },
                        new ItemTag { Value = "Urgent" }
                    ]
                },
                new ToDoItem { Title = "Water the plants", IsCompleted = true }
            });

            SaveChanges();
        }
    }
}
