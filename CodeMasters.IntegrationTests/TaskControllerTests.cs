using CodeMasters.Domain.Entities;
using CodeMasters.Infrastructure.Context;
using CodeMasters.Infrastructure.HttpClients;
using CodeMasters.Infrastructure.Repositories;
using CodeMasters.IntegrationTests.Factories;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace CodeMasters.IntegrationTests
{
    public class TaskControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly ChallengeContext _context;
        public TaskControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _context = TestDbContextFactory.CreateFakeDb();
        }

        [Fact]
        public async Task GetShouldReturnOk()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/task");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task ExecuteAsyncShouldReturnOk()
        {
            var client = _factory.CreateClient();

            var response = await client.PostAsync("/task", null);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task ExecuteAsyncShouldReturnNotFound()
        {
            var task = new ChallengeTask(Guid.NewGuid(), "addition", 0, 0);
            var challengeClientMock = new Mock<IChallengeClient>();
            challengeClientMock.Setup(client => client.GetTaskAsync()).ReturnsAsync(task);
            challengeClientMock.Setup(client => client.SubmitTaskAsync(task)).ThrowsAsync(await ApiExceptionFactory.CreateAsync(HttpStatusCode.NotFound));

            var client = _factory.WithWebHostBuilder(builder =>
                builder.ConfigureServices(services =>
                {
                    services.AddScoped(provider => challengeClientMock.Object);
                    services.AddScoped<ITaskRepository, TaskRepository>();
                    services.AddScoped(provider => _context);
                })
            ).CreateClient();

            var response = await client.PostAsync("/task", null);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ExecuteAsyncShouldReturnBadRequest()
        {
            var task = new ChallengeTask(Guid.NewGuid(), "addition", 0, 0);
            var challengeClientMock = new Mock<IChallengeClient>();
            challengeClientMock.Setup(client => client.GetTaskAsync()).ReturnsAsync(task);
            challengeClientMock.Setup(client => client.SubmitTaskAsync(task)).ThrowsAsync(await ApiExceptionFactory.CreateAsync(HttpStatusCode.BadRequest));

            var client = _factory.WithWebHostBuilder(builder =>
                builder.ConfigureServices(services =>
                {
                    services.AddScoped(provider => challengeClientMock.Object);
                    services.AddScoped<ITaskRepository, TaskRepository>();
                    services.AddScoped(provider => _context);
                })
            ).CreateClient();

            var response = await client.PostAsync("/task", null);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
