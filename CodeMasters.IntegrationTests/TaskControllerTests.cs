using CodeMasters.Domain.Aggregates.Tasks;
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
            _context = TestDbContextFactory.CreateFakeDb("TaskControllerTestsDb");
        }

        [Fact(DisplayName = "On get task should return ok http status code")]
        public async Task GetShouldReturnOk()
        {
            //Arrange
            var client = _factory.CreateClient();
            //Act
            var response = await client.GetAsync("/task");
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact(DisplayName = "On post task should return no content http status code")]
        public async Task ExecuteAsyncShouldReturnNoContent()
        {
            //Arrange
            var client = _factory.CreateClient();
            //Arrange
            var response = await client.PostAsync("/task", null);
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact(DisplayName = "On post task should return not found when ChallengeClient throws not found exception")]
        public async Task ExecuteAsyncShouldReturnNotFound()
        {
            //Arrange
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

            //Act
            var response = await client.PostAsync("/task", null);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact(DisplayName = "On post task should return bad request when ChallengeClient throws bad request exception")]
        public async Task ExecuteAsyncShouldReturnBadRequest()
        {
            //Arrange
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

            //Act
            var response = await client.PostAsync("/task", null);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
