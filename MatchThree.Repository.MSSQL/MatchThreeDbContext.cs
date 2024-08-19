using Microsoft.EntityFrameworkCore;

namespace MatchThree.Repository.MSSQL;

public class MatchThreeDbContext : DbContext
{
    public MatchThreeDbContext(DbContextOptions<MatchThreeDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MatchThreeDbContext).Assembly);
    }
}