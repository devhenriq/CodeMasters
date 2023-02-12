using CodeMasters.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CodeMasters.IntegrationTests.Factories
{
    public static class TestDbContextFactory
    {
        public static ChallengeContext CreateFakeDb()
        {
            var builder = new DbContextOptionsBuilder<ChallengeContext>();
            builder.UseInMemoryDatabase("TestChallengeDb");
            var context = new ChallengeContext(builder.Options);
            context.Database.EnsureDeleted();
            return context;
        }
    }
}
