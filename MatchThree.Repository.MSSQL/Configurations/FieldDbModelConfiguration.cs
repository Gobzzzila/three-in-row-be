using System.Text.Json;
using MatchThree.Repository.MSSQL.Configurations.Base;
using MatchThree.Repository.MSSQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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

        var converter = new ValueConverter<int[][], string>(
            v => JsonSerializer.Serialize(v, new JsonSerializerOptions { WriteIndented = false }),
            v => JsonSerializer.Deserialize<int[][]>(v, new JsonSerializerOptions { PropertyNameCaseInsensitive = false })!);

        builder.Property(e => e.Field)
            .HasConversion(converter);
        
        builder
            .HasOne(x => x.User)
            .WithOne(x => x.FieldElementLevel)
            .HasForeignKey<FieldDbModel>(x => x.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}