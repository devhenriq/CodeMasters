using CodeMasters.Domain.Entities;
using CodeMasters.Infrastructure.HttpClients;
using CodeMasters.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Contrib.WaitAndRetry;
using System.Net;

namespace CodeMasters.Infrastructure
{
    public static class Setup
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddChallengeClient(configuration);
        }

        private static void AddChallengeClient(this IServiceCollection services, IConfiguration configuration)
        {
            var challengeApiUrl = GetChallengeApiUrl(configuration);
            var delay = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(1), retryCount: 5);
            services.AddHttpClient(nameof(HttpClients.IChallengeClient), client => client.BaseAddress = new Uri(challengeApiUrl))
                            .AddPolicyHandler(GetRetryPolicy(delay))
                            .AddTypedClient(Refit.RestService.For<IChallengeClient>);
        }

        private static Func<IServiceProvider, HttpRequestMessage, IAsyncPolicy<HttpResponseMessage>> GetRetryPolicy(IEnumerable<TimeSpan> delay)
        {
            return (provider, msg) =>
            {
                return Policy.HandleResult<HttpResponseMessage>(message =>
                            message.StatusCode == HttpStatusCode.RequestTimeout ||
                            message.StatusCode == HttpStatusCode.BadGateway ||
                            message.StatusCode == HttpStatusCode.GatewayTimeout ||
                            message.StatusCode == HttpStatusCode.InternalServerError)
                        .WaitAndRetryAsync(delay);
            };
        }

        private static string GetChallengeApiUrl(IConfiguration configuration)
        {
            var challengeApiUrl = configuration["ChallengeApi"];
            if (string.IsNullOrEmpty(challengeApiUrl)) throw new ArgumentNullException(nameof(configuration), "Challenge API URL not found.");
            return challengeApiUrl;
        }

    }
}