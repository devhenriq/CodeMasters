using CodeMasters.Domain.Aggregates.Tasks;
using Refit;

namespace CodeMasters.Infrastructure.HttpClients
{
    public interface IChallengeClient
    {
        [Get("/get-task")]
        Task<ChallengeTask> GetTaskAsync();

        [Post("/submit-task")]
        Task SubmitTaskAsync(ChallengeTask request);
    }
}
