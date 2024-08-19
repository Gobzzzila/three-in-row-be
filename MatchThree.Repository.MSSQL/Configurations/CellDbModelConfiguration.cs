using MatchThree.Repository.MSSQL.Configurations.Base;
using MatchThree.Repository.MSSQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatchThree.Repository.MSSQL.Configurations;

public class CellDbModelConfiguration : EntityTypeConfigurationBase<CellDbModel>
{
    protected override void ConfigureEntityProperties(EntityTypeBuilder<CellDbModel> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .HasIndex(x => new { x.UserId, X = x.CoordinateX, Y = x.CoordinateY })
            .IsUnique();

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Cells)
            .HasForeignKey(x => x.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }

}