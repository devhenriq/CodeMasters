using CodeMasters.Domain.Aggregates.Tasks;
using CodeMasters.Domain.Entities;
using CodeMasters.Domain.Factories;

namespace CodeMasters.Domain.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IOperationFactory _factory;
        public TaskService(IOperationFactory factory, ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
            _factory = factory;
        }

        public async Task ExecuteAsync()
        {
            var challengeTask = await _taskRepository.GetAsync();
            var operation = _factory.CreateOperation(challengeTask.Operation);
            var result = operation.Calculate(challengeTask.Left, challengeTask.Right);
            challengeTask.SetResult(result);
            await _taskRepository.SubmitAsync(challengeTask);
        }

        public async Task<IEnumerable<ChallengeTask>> GetExecutedTasks()
        {
            return await _taskRepository.GetExecutedTasks();
        }
    }
}
