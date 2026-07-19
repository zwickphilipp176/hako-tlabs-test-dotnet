using System.Linq;
using TestApp.ToDoList.Domain.Entity;

namespace TestApp.ToDoList.Infrastructure.Store
{
    /// <summary>
    /// Seeds the <see cref="ToDoListDbContext"/> with initial to-do items.
    /// </summary>
    public static class ToDoListDbSeeder
    {
        /// <summary>
        /// Seeds the database with initial to-do items, unless it has already been seeded.
        /// </summary>
        /// <param name="context"></param>
        public static void Seed(IToDoListDbContext context)
        {
            if (context.ToDoItems.Any())
                return;

            context.ToDoItems.AddRange(new[]
            {
                new ToDoItem
                {
                    Title = "Laundry",
                    Tags = [
                        new ItemTag { Value = "Household" }
                    ]
                },
                new ToDoItem
                {
                    Title = "Grocery Shopping",
                    IsCompleted = true,
                    Tags = [
                        new ItemTag { Value = "Finance" }
                    ]
                },
                new ToDoItem
                {
                    Title = "Pay Bills",
                    Tags = [
                        new ItemTag { Value = "Finance" },
                        new ItemTag { Value = "Urgent" }
                    ]
                },
                new ToDoItem { Title = "Clean the House", IsCompleted = true }
            });
            context.SaveChanges();
        }
    }
}
