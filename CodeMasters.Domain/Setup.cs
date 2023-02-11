using CodeMasters.Domain.Aggregates.Tasks;
using CodeMasters.Domain.Factories;
using CodeMasters.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CodeMasters.Domain
{
    public static class Setup
    {
        public static void AddDomain(this IServiceCollection services)
        {
            services.AddScoped<IOperationFactory, OperationFactory>();
            services.AddScoped<ITaskService, TaskService>();
        }
    }
}