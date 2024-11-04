using MatchThree.Repository.MSSQL.Configurations.Base;
using MatchThree.Repository.MSSQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatchThree.Repository.MSSQL.Configurations;

public class DailyLoginDbModelConfiguration : EntityTypeConfigurationBase<DailyLoginDbModel>
{
    protected override void ConfigureEntityProperties(EntityTypeBuilder<DailyLoginDbModel> builder)
    {
        builder
            .HasKey(x => x.Id)
            .IsClustered();
        builder
            .Property(x => x.Id)
            .ValueGeneratedNever();
    }
}