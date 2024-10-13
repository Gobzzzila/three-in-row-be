using MatchThree.Repository.MSSQL.Configurations.Base;
using MatchThree.Repository.MSSQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatchThree.Repository.MSSQL.Configurations;

public class FieldElementLevelDbModelConfiguration : EntityTypeConfigurationBase<FieldElementsDbModel>
{
    protected override void ConfigureEntityProperties(EntityTypeBuilder<FieldElementsDbModel> builder)
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
            .HasForeignKey<FieldElementsDbModel>(x => x.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}