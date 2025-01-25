using MatchThree.Repository.MSSQL.Configurations.Base;
using MatchThree.Repository.MSSQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatchThree.Repository.MSSQL.Configurations;

public class NotificationsDbModelConfiguration : EntityTypeConfigurationBase<NotificationsDbModel> 
{
    protected override void ConfigureEntityProperties(EntityTypeBuilder<NotificationsDbModel> builder)
    {
        builder
            .HasKey(x => x.Id)
            .IsClustered();
        builder
            .Property(x => x.Id)
            .ValueGeneratedNever();
        
        builder
            .HasOne(x => x.User)
            .WithOne(x => x.Notifications)
            .HasForeignKey<NotificationsDbModel>(x => x.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        
        builder
            .Property(x => x.EnergyNotification)
            .HasDefaultValue(null);
        
        builder
            .HasIndex(x => x.EnergyNotification)
            .IsUnique(false);
    }
}