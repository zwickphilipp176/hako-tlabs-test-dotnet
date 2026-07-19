using Microsoft.AspNetCore.Mvc;
using System.Net;
using TestApp.Server.Model;
using TestApp.ToDoList.Application;
using TestApp.ToDoList.Application.Common;
using TestApp.ToDoList.Application.Model;
using TestApp.ToDoList.Application.Model.TodoItem;

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

        [HttpGet("{id}")]
        [ProducesResponseType<ToDoItemDto>(200)]
        [ProducesResponseType<ErrorResult>((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ToDoItemDto>> GetTask(int id)
        {
            ToDoItemDto task = null;
            try
            {
                task = await toDoListTracker.GetItemAsync(id);
            }
            catch (NotFoundException notFound)
            {
                return NotFound(new ErrorResult(notFound.Message));
            }

            return Ok(task);
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<ToDoItemDto>>> GetTasks([FromQuery] GetToDoItemsQuery queryOptions)
        {
            var tasks = await toDoListTracker.GetAllItemsAsync(queryOptions);
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<ActionResult<ToDoItemDto>> CreateTask([FromBody] CreateToDoItem item)
        {
            var task = await toDoListTracker.AddItemAsync(item);
            return Created($"{task.Id}", task);
        }

        [HttpPut("{id}")]
        [ProducesResponseType<ToDoItemDto>(200)]
        [ProducesResponseType<ErrorResult>((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ToDoItemDto>> EditTask(int id, [FromBody] EditToDoItem item)
        {
            ToDoItemDto task = null;
            try
            {
                task = await toDoListTracker.EditItemAsync(id, item);
            }
            catch (NotFoundException notFound)
            {
                return NotFound(new ErrorResult(notFound.Message));
            }

            return Ok(task);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType<ToDoItemDto>(200)]
        [ProducesResponseType<ErrorResult>((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ToDoItemDto>> DeleteTask(int id)
        {
            ToDoItemDto task = null;
            try
            {
                task = await toDoListTracker.DeleteItemAsync(id);

            }
            catch (NotFoundException notFound)
            {
                return NotFound(new ErrorResult(notFound.Message));
            }

            return Ok(task);
        }

        [HttpPost("{id}/tags/add")]
        [ProducesResponseType<ToDoItemDto>(200)]
        [ProducesResponseType<ErrorResult>((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType<ErrorResult>((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ToDoItemDto>> AddTagToTask(int id, [FromBody] string tagValue)
        {
            ToDoItemDto task = null;
            try
            {
                task = await toDoListTracker.AddTagToItemAsync(id, tagValue);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ErrorResult(ex.Message));
            }
            catch (NotFoundException notFound)
            {
                return NotFound(new ErrorResult(notFound.Message));
            }

            return Ok(task);
        }

        [HttpDelete("{id}/tags/delete")]
        [ProducesResponseType<ToDoItemDto>(200)]
        [ProducesResponseType<ErrorResult>((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ToDoItemDto>> DeleteTagFromTask(int id, [FromBody] string tagValue)
        {
            ToDoItemDto task = null;
            try
            {
                task = await toDoListTracker.DeleteTagFromItemAsync(id, tagValue);
            }
            catch (NotFoundException notFound)
            {
                return NotFound(new ErrorResult(notFound.Message));
            }

            return Ok(task);
        }
    }
}