using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MatchThree.Repository.MSSQL;

#if DEBUG
public class CookBookDbContextFactory : IDesignTimeDbContextFactory<MatchThreeDbContext>
{
    public MatchThreeDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MatchThreeDbContext>();
        optionsBuilder.UseSqlServer("Server=.;Initial Catalog=ToolManagementDB;Integrated Security=True;TrustServerCertificate=True");

        return new MatchThreeDbContext(optionsBuilder.Options);
    }
}
#endif