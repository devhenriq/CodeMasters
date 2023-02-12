using CodeMasters.Domain.Aggregates.Tasks;
using CodeMasters.Domain.Exceptions;
using CodeMasters.Infrastructure.Context;
using CodeMasters.Infrastructure.HttpClients;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Refit;
using System.Net;
using System.Text.Json;

namespace CodeMasters.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IChallengeClient _challengeApi;
        private readonly ChallengeContext _challengeContext;
        private readonly ILogger<TaskRepository> _logger;
        public TaskRepository(IChallengeClient challengeApi, ChallengeContext challengeContext, ILogger<TaskRepository> logger)
        {
            _challengeApi = challengeApi;
            _challengeContext = challengeContext;
            _logger = logger;
        }

        public async Task<ChallengeTask> GetAsync()
        {
            var task = await _challengeApi.GetTaskAsync();
            _logger.LogInformation($"Get task, input: {JsonSerializer.Serialize(task)}");

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
                _logger.LogInformation($"Submit task, input: {JsonSerializer.Serialize(task)}");
                await _challengeApi.SubmitTaskAsync(task);
                _challengeContext.SaveChanges();
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex.Message);
                if (ex.StatusCode == HttpStatusCode.BadRequest)
                    throw new InvalidInputException("Submit task");
                if (ex.StatusCode == HttpStatusCode.NotFound)
                    throw new EntityNotFoundException("Task");
            }
        }
    }
}
