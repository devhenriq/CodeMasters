using CodeMasters.Domain.Entities;
using CodeMasters.Domain.Exceptions;
using CodeMasters.Infrastructure.Context;
using CodeMasters.Infrastructure.HttpClients;
using CodeMasters.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace CodeMasters.IntegrationTests
{
    public class TaskRepositoryTests
    {
        private readonly ChallengeContext _context;
        private readonly Faker _faker;
        private readonly SubmitTaskRequest _submitTaskRequest;
        public TaskRepositoryTests()
        {
            var builder = new DbContextOptionsBuilder<ChallengeContext>();
            builder.UseInMemoryDatabase("TestChallengeDb");
            _context = new ChallengeContext(builder.Options);
            _context.Database.EnsureDeleted();

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
            challengeClientMock.Setup(client => client.SubmitTaskAsync(new SubmitTaskRequest(expectedTask.Id, expectedTask.Left * expectedTask.Right))).Returns(Task.CompletedTask);

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
            challengeClientMock.Setup(client => client.SubmitTaskAsync(new SubmitTaskRequest(expectedTask.Id, expectedTask.Left * expectedTask.Right))).ThrowsAsync(new InvalidInputException("Submit task"));

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
            challengeClientMock.Setup(client => client.SubmitTaskAsync(new SubmitTaskRequest(expectedTask.Id, expectedTask.Left * expectedTask.Right))).ThrowsAsync(new EntityNotFoundException("Task"));

            var taskRepository = new TaskRepository(challengeClientMock.Object, _context, new Mock<ILogger<TaskRepository>>().Object);
            var act = async () => await taskRepository.SubmitAsync(expectedTask);
            await act.Should().ThrowAsync<EntityNotFoundException>();
        }
        [Fact]
        public async Task SubmitAsyncShouldThrowInvalidInputExceptionWhenResultIsEmpty()
        {
            var expectedTask = new ChallengeTask(Guid.NewGuid(), "multiplication", _faker.Random.Double(), _faker.Random.Double());
            var challengeClientMock = new Mock<IChallengeClient>();
            var taskRepository = new TaskRepository(challengeClientMock.Object, _context, new Mock<ILogger<TaskRepository>>().Object);
            var act = async () => await taskRepository.SubmitAsync(expectedTask);
            await act.Should().ThrowAsync<InvalidInputException>();
        }
    }
}