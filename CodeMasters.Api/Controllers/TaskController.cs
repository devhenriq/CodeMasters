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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ChallengeTask>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetExecutedTasksAsync()
        {
            return Ok(await _taskService.GetExecutedTasks());
        }

        [Time]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> ExecuteAsync()
        {
            await _taskService.ExecuteAsync();
            return NoContent();
        }
    }
}