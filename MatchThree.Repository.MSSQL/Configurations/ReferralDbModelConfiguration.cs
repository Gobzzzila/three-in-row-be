using MatchThree.Repository.MSSQL.Configurations.Base;
using MatchThree.Repository.MSSQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatchThree.Repository.MSSQL.Configurations;

public class ReferralDbModelConfiguration : EntityTypeConfigurationBase<ReferralDbModel>
{
    protected override void ConfigureEntityProperties(EntityTypeBuilder<ReferralDbModel> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .HasIndex(x => new { x.ReferrerUserId, x.ReferralUserId })
            .IsUnique();
        
        builder
            .HasOne(x => x.Referrer)
            .WithMany(x => x.Referrals)
            .HasForeignKey(x => x.ReferrerUserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        
        builder
            .HasOne(x => x.Referral)
            .WithOne(x => x.Referrer)
            .HasForeignKey<ReferralDbModel>(x => x.ReferralUserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}