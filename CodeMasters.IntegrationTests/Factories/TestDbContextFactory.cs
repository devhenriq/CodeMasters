using CodeMasters.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CodeMasters.IntegrationTests.Factories
{
    public static class TestDbContextFactory
    {
        public static ChallengeContext CreateFakeDb(string dbName)
        {
            var builder = new DbContextOptionsBuilder<ChallengeContext>();
            builder.UseInMemoryDatabase(dbName);
            var context = new ChallengeContext(builder.Options);
            context.Database.EnsureDeleted();
            return context;
        }
    }
}
