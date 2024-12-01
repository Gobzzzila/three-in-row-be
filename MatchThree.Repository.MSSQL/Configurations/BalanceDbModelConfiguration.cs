using MatchThree.Repository.MSSQL.Configurations.Base;
using MatchThree.Repository.MSSQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatchThree.Repository.MSSQL.Configurations;

public class BalanceDbModelConfiguration : EntityTypeConfigurationBase<BalanceDbModel>
{
    protected override void ConfigureEntityProperties(EntityTypeBuilder<BalanceDbModel> builder)
    {
        builder
            .HasKey(x => x.Id)
            .IsClustered();
        builder
            .Property(x => x.Id)
            .ValueGeneratedNever();
        
        builder
            .HasIndex(x => x.OverallBalance);
        
        builder
            .HasOne(x => x.User)
            .WithOne(x => x.Balance)
            .HasForeignKey<BalanceDbModel>(x => x.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}