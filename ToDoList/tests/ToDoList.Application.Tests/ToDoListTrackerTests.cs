using TestApp.ToDoList.Application;
using TestApp.ToDoList.Application.Common;
using TestApp.ToDoList.Application.Model.TodoItem;
using TestApp.ToDoList.Application.Services;
using TestApp.ToDoList.Infrastructure.Repositories;
using ToDoList.Application.Tests.Store;

namespace ToDoList.Application.Tests
{
    /// <summary>
    /// Functional tests for <see cref="ToDoListTracker"/> backed by a dedicated EF Core in-memory test database.
    /// </summary>
    public class ToDoListTrackerTests
    {
        private TestToDoListDbContext dbContext;
        private ToDoListTracker tracker;

        [SetUp]
        public void Setup()
        {
            // Each test gets its own uniquely named in-memory database, fully isolated from other tests
            // and decoupled from the production ToDoListDbContext configuration.
            dbContext = new TestToDoListDbContext();
            dbContext.SeedTestData();

            var todoItemsRepo = new ToDoItemsRepository(dbContext);
            var itemTagsRepo = new ItemTagsRepository(dbContext);

            tracker = new ToDoListTracker(todoItemsRepo, itemTagsRepo);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        [Test]
        public async Task AddItemAsync_AddsNewItem_AndReturnsDto()
        {
            var result = await tracker.AddItemAsync(new CreateToDoItem("Buy milk"));

            Assert.That(result.Id, Is.GreaterThan(0));
            Assert.That(result.Title, Is.EqualTo("Buy milk"));
            Assert.That(result.IsCompleted, Is.False);
            Assert.That(result.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(result.CompletedAt, Is.Null);

            var fetched = await tracker.GetItemAsync(result.Id);
            Assert.That(fetched.Title, Is.EqualTo("Buy milk"));
        }

        [Test]
        public async Task GetItemAsync_ThrowsNotFoundException_WhenItemDoesNotExist()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await tracker.GetItemAsync(int.MaxValue));
        }

        [Test]
        public async Task GetAllItemsAsync_ReturnsSeededItems()
        {
            var result = await tracker.GetAllItemsAsync(new GetToDoItemsQuery(null, null, null, null));

            Assert.That(result.TotalCount, Is.EqualTo(4));
            Assert.That(result.Items.Select(x => x.Title), Is.EquivalentTo(new[]
            {
                "Write unit tests", "Review pull request", "Deploy release", "Water the plants"
            }));
        }

        [Test]
        public async Task GetAllItemsAsync_FiltersBySearchTerm()
        {
            var query = new GetToDoItemsQuery(
                new GetToDoItemsQueryFilter("Deploy", null, null),
                null,
                null,
                null);

            var result = await tracker.GetAllItemsAsync(query);

            Assert.That(result.TotalCount, Is.EqualTo(1));
            Assert.That(result.Items.Single().Title, Is.EqualTo("Deploy release"));
        }

        [Test]
        public async Task EditItemAsync_UpdatesTitleAndCompletionState()
        {
            var created = await tracker.AddItemAsync(new CreateToDoItem("Old title"));

            var updated = await tracker.EditItemAsync(created.Id, new EditToDoItem("New title", true));

            Assert.That(updated.Title, Is.EqualTo("New title"));
            Assert.That(updated.IsCompleted, Is.True);
            Assert.That(updated.CompletedAt, Is.Not.Null);
        }

        [Test]
        public void EditItemAsync_ThrowsNotFoundException_WhenItemDoesNotExist()
        {
            Assert.ThrowsAsync<NotFoundException>(async () =>
                await tracker.EditItemAsync(int.MaxValue, new EditToDoItem("Title", false)));
        }

        [Test]
        public async Task DeleteItemAsync_RemovesItem_AndReturnsDeletedDto()
        {
            var created = await tracker.AddItemAsync(new CreateToDoItem("To be removed"));

            var deleted = await tracker.DeleteItemAsync(created.Id);

            Assert.That(deleted.Id, Is.EqualTo(created.Id));
            Assert.ThrowsAsync<NotFoundException>(async () => await tracker.GetItemAsync(created.Id));
        }

        [Test]
        public void DeleteItemAsync_ThrowsNotFoundException_WhenItemDoesNotExist()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await tracker.DeleteItemAsync(int.MaxValue));
        }

        [Test]
        public async Task AddTagToItemAsync_AddsTag_WhenNotAlreadyPresent()
        {
            var created = await tracker.AddItemAsync(new CreateToDoItem("Item with tag"));

            var updated = await tracker.AddTagToItemAsync(created.Id, "Personal");

            Assert.That(updated.Tags, Does.Contain("Personal"));
        }

        [Test]
        public async Task AddTagToItemAsync_ThrowsInvalidOperationException_WhenTagAlreadyExists()
        {
            var created = await tracker.AddItemAsync(new CreateToDoItem("Item with tag"));
            await tracker.AddTagToItemAsync(created.Id, "Personal");

            Assert.ThrowsAsync<System.InvalidOperationException>(async () =>
                await tracker.AddTagToItemAsync(created.Id, "personal"));
        }

        [Test]
        public void AddTagToItemAsync_ThrowsNotFoundException_WhenItemDoesNotExist()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await tracker.AddTagToItemAsync(int.MaxValue, "Tag"));
        }

        [Test]
        public async Task DeleteTagFromItemAsync_RemovesExistingTag()
        {
            var created = await tracker.AddItemAsync(new CreateToDoItem("Item with tag"));
            await tracker.AddTagToItemAsync(created.Id, "Personal");

            var updated = await tracker.DeleteTagFromItemAsync(created.Id, "Personal");

            Assert.That(updated.Tags, Does.Not.Contain("Personal"));
        }

        [Test]
        public async Task DeleteTagFromItemAsync_ThrowsNotFoundException_WhenTagDoesNotExist()
        {
            var created = await tracker.AddItemAsync(new CreateToDoItem("Item without tag"));

            Assert.ThrowsAsync<NotFoundException>(async () =>
                await tracker.DeleteTagFromItemAsync(created.Id, "Missing"));
        }

        [Test]
        public void DeleteTagFromItemAsync_ThrowsNotFoundException_WhenItemDoesNotExist()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await tracker.DeleteTagFromItemAsync(int.MaxValue, "Tag"));
        }
    }
}
