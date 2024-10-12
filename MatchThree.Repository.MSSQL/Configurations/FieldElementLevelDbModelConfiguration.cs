using MatchThree.Repository.MSSQL.Configurations.Base;
using MatchThree.Repository.MSSQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatchThree.Repository.MSSQL.Configurations;

public class FieldElementLevelDbModelConfiguration : EntityTypeConfigurationBase<FieldElementLevelDbModel>
{
    protected override void ConfigureEntityProperties(EntityTypeBuilder<FieldElementLevelDbModel> builder)
    {
        builder
            .HasKey(x => x.Id)
            .IsClustered();
        builder
            .Property(x => x.Id)
            .ValueGeneratedNever();

        builder
            .HasOne(x => x.User)
            .WithOne(x => x.FieldElementLevel)
            .HasForeignKey<FieldElementLevelDbModel>(x => x.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}