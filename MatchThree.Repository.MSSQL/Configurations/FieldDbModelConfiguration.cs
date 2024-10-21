using MatchThree.Repository.MSSQL.Configurations.Base;
using MatchThree.Repository.MSSQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatchThree.Repository.MSSQL.Configurations;

public class FieldDbModelConfiguration : EntityTypeConfigurationBase<FieldDbModel>
{
    protected override void ConfigureEntityProperties(EntityTypeBuilder<FieldDbModel> builder)
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
            .HasForeignKey<FieldDbModel>(x => x.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}