using Microsoft.AspNetCore.Mvc;
using TestApp.ToDoList.Application.Common;
using TestApp.ToDoList.Application.Model;
using TestApp.ToDoList.Domain.Entity;

namespace TestApp.Server.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class ToDoTasksController : ControllerBase
    {
        private readonly IToDoListTracker toDoListTracker;

        public ToDoTasksController(IToDoListTracker toDoListTracker)
        {
            this.toDoListTracker = toDoListTracker;
        }

        [HttpGet]
        public async Task<ActionResult<IList<ToDoItem>>> GetTasks()
        {
            var tasks = await toDoListTracker.GetAllItemsAsync();
            return tasks.ToList();
        }

        [HttpPost]
        public async Task<ActionResult<ToDoItem>> CreateTask([FromBody] CreateToDoItem item)
        {
            var task = await toDoListTracker.AddItemAsync(item);
            return task;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ToDoItem>> EditTask(int id, [FromBody] EditToDoItem item)
        {
            var task = await toDoListTracker.EditItemAsync(id, item);
            if (task == null)
                return NotFound();

            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ToDoItem>> DeleteTask(int id)
        {
            var task = await toDoListTracker.RemoveItemAsync(id);
            if (task == null)
                return NotFound();

            return Ok(task);
        }
    }
}