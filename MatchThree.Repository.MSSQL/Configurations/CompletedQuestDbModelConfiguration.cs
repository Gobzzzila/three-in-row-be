using System.Text.Json;
using MatchThree.Repository.MSSQL.Configurations.Base;
using MatchThree.Repository.MSSQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MatchThree.Repository.MSSQL.Configurations;

public class CompletedQuestDbModelConfiguration : EntityTypeConfigurationBase<CompletedQuestsDbModel>
{
    protected override void ConfigureEntityProperties(EntityTypeBuilder<CompletedQuestsDbModel> builder)
    {
        builder
            .HasKey(x => x.Id)
            .IsClustered();
        builder
            .Property(x => x.Id)
            .ValueGeneratedNever();
        
        var converter = new ValueConverter<List<Guid>, string>(
            v => JsonSerializer.Serialize(v, new JsonSerializerOptions { WriteIndented = false }),
            v => JsonSerializer.Deserialize<List<Guid>>(v, new JsonSerializerOptions { PropertyNameCaseInsensitive = false })!);
        
        var comparer = new ValueComparer<List<Guid>>((c1, c2) => c1.SequenceEqual(c2), 
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), 
            c => c.ToList());

        builder.Property(e => e.QuestIds)
            .HasConversion(converter)
            .Metadata.SetValueComparer(comparer);
    }
}