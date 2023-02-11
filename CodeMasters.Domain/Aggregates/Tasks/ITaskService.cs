using CodeMasters.Domain.Entities;

namespace CodeMasters.Domain.Aggregates.Tasks
{
    public interface ITaskService
    {
        Task<IEnumerable<ChallengeTask>> GetExecutedTasks();
        Task ExecuteAsync();
    }
}
