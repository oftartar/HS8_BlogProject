using HS8_BlogProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HS8_BlogProject.Infrastructure.EntityTypeConfig
{
    internal abstract class BaseEntityConfig<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IBaseEntity
    {
        public virtual void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.CreateDate).IsRequired(true);
            builder.Property(x => x.DeleteDate).IsRequired(false);
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.Status).IsRequired(true);
        }
    }
}
