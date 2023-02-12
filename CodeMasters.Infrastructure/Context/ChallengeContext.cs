using CodeMasters.Domain.Aggregates.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CodeMasters.Infrastructure.Context
{
    public class ChallengeContext : DbContext
    {
        public ChallengeContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ChallengeTask> Tasks => Set<ChallengeTask>();
    }
}
