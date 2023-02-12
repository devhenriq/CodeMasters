using CodeMasters.Domain.Aggregates.Tasks;
using CodeMasters.Domain.Factories;
using CodeMasters.Domain.Handlers;
using CodeMasters.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CodeMasters.Domain
{
    public static class Setup
    {
        public static void AddDomain(this IServiceCollection services)
        {
            services.AddScoped<IOperationFactory, OperationFactory>();
            services.AddScoped<ITaskService, TaskService>();
        }

        public static void UseDomain(this IApplicationBuilder app, ILogger logger)
        {
            app.AddExceptionHandler(logger);
        }
    }
}