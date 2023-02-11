namespace CodeMasters.Domain.Entities
{
    public interface ITaskRepository
    {
        Task<ChallengeTask> GetAsync();
        Task SubmitAsync(Guid id, decimal result);
    }
}
