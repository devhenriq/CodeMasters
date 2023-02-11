using CodeMasters.Domain.Entities;
using CodeMasters.Infrastructure.HttpClients;

namespace CodeMasters.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IChallengeClient _challengeApi;
        //adicionar in memory db aqui
        public TaskRepository(IChallengeClient challengeApi)
        {
            _challengeApi = challengeApi;
        }

        public async Task<ChallengeTask> GetAsync()
        {
            return await _challengeApi.GetTaskAsync();
        }

        public async Task SubmitAsync(Guid id, decimal result)
        {
            await _challengeApi.SubmitTaskAsync(new SubmitTaskRequest(id, result));
        }
    }
}
