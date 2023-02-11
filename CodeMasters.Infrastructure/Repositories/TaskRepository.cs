using CodeMasters.Domain.Entities;
using CodeMasters.Domain.Exceptions;
using CodeMasters.Infrastructure.Context;
using CodeMasters.Infrastructure.HttpClients;
using Microsoft.EntityFrameworkCore;
using Refit;
using System.Net;

namespace CodeMasters.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IChallengeClient _challengeApi;
        private readonly ChallengeContext _challengeContext;
        public TaskRepository(IChallengeClient challengeApi, ChallengeContext challengeContext)
        {
            _challengeApi = challengeApi;
            _challengeContext = challengeContext;
        }

        public async Task<ChallengeTask> GetAsync()
        {
            var task = await _challengeApi.GetTaskAsync();
            _challengeContext.Add(task);
            _challengeContext.SaveChanges();
            return task;
        }

        public async Task<IEnumerable<ChallengeTask>> GetExecutedTasks()
        {
            return await _challengeContext.Tasks.ToListAsync();
        }

        public async Task SubmitAsync(ChallengeTask task)
        {
            try
            {
                await _challengeApi.SubmitTaskAsync(new SubmitTaskRequest(task.Id, task.Result));
                _challengeContext.SaveChanges();
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == HttpStatusCode.BadRequest)
                    throw new InvalidInputException("Submit task");
                if (ex.StatusCode == HttpStatusCode.NotFound)
                    throw new EntityNotFoundException("Task");
            }
        }
    }
}
