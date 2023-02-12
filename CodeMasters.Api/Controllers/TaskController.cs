using CodeMasters.Domain.Aggregates.Tasks;
using MethodTimer;
using Microsoft.AspNetCore.Mvc;

namespace CodeMasters.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetExecutedTasksAsync()
        {
            return Ok(await _taskService.GetExecutedTasks());
        }

        [Time]
        [HttpPost]
        public async Task<IActionResult> ExecuteAsync()
        {
            await _taskService.ExecuteAsync();
            return NoContent();
        }
    }
}