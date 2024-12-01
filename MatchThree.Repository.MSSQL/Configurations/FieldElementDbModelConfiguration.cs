using MatchThree.Repository.MSSQL.Configurations.Base;
using MatchThree.Repository.MSSQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatchThree.Repository.MSSQL.Configurations;

public class FieldElementDbModelConfiguration : EntityTypeConfigurationBase<FieldElementDbModel>
{
    protected override void ConfigureEntityProperties(EntityTypeBuilder<FieldElementDbModel> builder)
    {
        builder
            .HasIndex(x => x.UserId);
        
        builder
            .HasIndex(x => new { x.UserId, x.Element })
            .IsUnique();

        builder
            .HasOne(x => x.Field)
            .WithMany(x => x.FieldElements)
            .HasForeignKey(x => x.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}