using MatchThree.Repository.MSSQL.Configurations.Base;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatchThree.Repository.MSSQL.Configurations;

public class UserSettingsDbModelConfiguration : EntityTypeConfigurationBase<UserSettingsDbModel>
{
    protected override void ConfigureEntityProperties(EntityTypeBuilder<UserSettingsDbModel> builder)
    {
        builder
            .HasKey(x => x.Id)
            .IsClustered();
        builder
            .Property(x => x.Id)
            .ValueGeneratedNever();
        
        builder
            .HasOne(x => x.User)
            .WithOne(x => x.Settings)
            .HasForeignKey<UserSettingsDbModel>(x => x.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        
        builder
            .Property(x => x.Notifications)
            .HasDefaultValue(true);
        
        builder
            .Property(x => x.Culture)
            .HasDefaultValue(CultureTypes.En);
    }
}