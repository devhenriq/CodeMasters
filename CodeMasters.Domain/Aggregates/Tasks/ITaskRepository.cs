namespace CodeMasters.Domain.Aggregates.Tasks
{
    public interface ITaskRepository
    {
        Task<ChallengeTask> GetAsync();
        Task<IEnumerable<ChallengeTask>> GetExecutedTasks();
        Task SubmitAsync(ChallengeTask task);
    }
}
