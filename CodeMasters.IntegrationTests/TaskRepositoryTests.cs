using CodeMasters.Domain.Entities;
using CodeMasters.Domain.Exceptions;
using CodeMasters.Infrastructure.Context;
using CodeMasters.Infrastructure.HttpClients;
using CodeMasters.Infrastructure.Repositories;
using CodeMasters.IntegrationTests.Factories;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CodeMasters.IntegrationTests
{
    public class TaskRepositoryTests
    {
        private readonly ChallengeContext _context;
        private readonly Faker _faker;
        public TaskRepositoryTests()
        {
            _context = TestDbContextFactory.CreateFakeDb();
            _faker = new Faker();
        }

        [Fact]
        public async Task GetAsyncShouldReturnChallengeClientTask()
        {
            var expectedTask = new ChallengeTask(Guid.NewGuid(), "multiplication", _faker.Random.Double(), _faker.Random.Double());

            var challengeClientMock = new Mock<IChallengeClient>();
            challengeClientMock.Setup(client => client.GetTaskAsync()).ReturnsAsync(expectedTask);
            var taskRepository = new TaskRepository(challengeClientMock.Object, _context, new Mock<ILogger<TaskRepository>>().Object);
            var task = await taskRepository.GetAsync();
            task.Id.Should().Be(expectedTask.Id);
        }

        [Fact]
        public async Task GetAsyncShouldSaveChallengeClientTaskOnDabase()
        {
            var expectedTask = new ChallengeTask(Guid.NewGuid(), "multiplication", _faker.Random.Double(), _faker.Random.Double());
            var challengeClientMock = new Mock<IChallengeClient>();
            challengeClientMock.Setup(client => client.GetTaskAsync()).ReturnsAsync(expectedTask);
            var taskRepository = new TaskRepository(challengeClientMock.Object, _context, new Mock<ILogger<TaskRepository>>().Object);
            await taskRepository.GetAsync();

            _context.Tasks.FirstOrDefault(task => task.Id == expectedTask.Id).Should().NotBeNull();
        }

        [Fact]
        public async Task SubmitAsyncShouldSaveChallengeClientResultOnDabatase()
        {
            var expectedTask = new ChallengeTask(Guid.NewGuid(), "multiplication", _faker.Random.Double(), _faker.Random.Double());
            _context.Add(expectedTask);
            _context.SaveChanges();
            expectedTask.SetResult(expectedTask.Left * expectedTask.Right);

            var challengeClientMock = new Mock<IChallengeClient>();
            challengeClientMock.Setup(client => client.SubmitTaskAsync(expectedTask)).Returns(Task.CompletedTask);

            var taskRepository = new TaskRepository(challengeClientMock.Object, _context, new Mock<ILogger<TaskRepository>>().Object);
            await taskRepository.SubmitAsync(expectedTask);

            _context.Tasks.First(task => task.Id == expectedTask.Id).Result.Should().Be(expectedTask.Result);
        }

        [Fact]
        public async Task SubmitAsyncShouldThrowInvalidInputExceptionWhenChallengeClientThrowsException()
        {
            var expectedTask = new ChallengeTask(Guid.NewGuid(), "multiplication", _faker.Random.Double(), _faker.Random.Double());
            _context.Add(expectedTask);
            _context.SaveChanges();
            expectedTask.SetResult(_faker.Random.Double());
            var challengeClientMock = new Mock<IChallengeClient>();
            challengeClientMock.Setup(client => client.SubmitTaskAsync(expectedTask)).ThrowsAsync(await ApiExceptionFactory.CreateAsync(HttpStatusCode.BadRequest));

            var taskRepository = new TaskRepository(challengeClientMock.Object, _context, new Mock<ILogger<TaskRepository>>().Object);
            var act = async () => await taskRepository.SubmitAsync(expectedTask);
            await act.Should().ThrowAsync<InvalidInputException>();
        }

        [Fact]
        public async Task SubmitAsyncShouldThrowEntityNotFoundExceptionWhenChallengeClientThrowsException()
        {
            var expectedTask = new ChallengeTask(Guid.NewGuid(), "multiplication", _faker.Random.Double(), _faker.Random.Double());
            _context.Add(expectedTask);
            _context.SaveChanges();
            expectedTask.SetResult(_faker.Random.Double());

            var challengeClientMock = new Mock<IChallengeClient>();
            challengeClientMock.Setup(client => client.SubmitTaskAsync(expectedTask)).ThrowsAsync(await ApiExceptionFactory.CreateAsync(HttpStatusCode.NotFound));

            var taskRepository = new TaskRepository(challengeClientMock.Object, _context, new Mock<ILogger<TaskRepository>>().Object);
            var act = async () => await taskRepository.SubmitAsync(expectedTask);
            await act.Should().ThrowAsync<EntityNotFoundException>();
        }
    }
}