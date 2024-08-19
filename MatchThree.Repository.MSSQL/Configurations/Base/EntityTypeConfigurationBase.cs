using MatchThree.Repository.MSSQL.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MatchThree.Repository.MSSQL.Configurations.Base;

public abstract class EntityTypeConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : class, IDbModel
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        ConfigureEntityProperties(builder);
        ConfigureDefaultEntitySeedData(builder);
    }

    protected abstract void ConfigureEntityProperties(EntityTypeBuilder<TEntity> builder);

    protected virtual void ConfigureDefaultEntitySeedData(EntityTypeBuilder<TEntity> builder)
    {
    }
}