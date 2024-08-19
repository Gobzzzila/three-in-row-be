using MatchThree.Repository.MSSQL.Configurations.Base;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatchThree.Repository.MSSQL.Configurations;

public class UserDbModelConfiguration : EntityTypeConfigurationBase<UserDbModel>
{
    protected override void ConfigureEntityProperties(EntityTypeBuilder<UserDbModel> builder)
    {
        builder
            .HasKey(x => x.Id)
            .IsClustered();
        builder
            .Property(x => x.Id)
            .ValueGeneratedNever();

        builder
            .Property(x => x.FirstName)
            .HasMaxLength(UserConstants.FirstNameMaxLength);

        builder
            .Property(x => x.Username)
            .HasMaxLength(UserConstants.UsernameMaxLength);
    }
}