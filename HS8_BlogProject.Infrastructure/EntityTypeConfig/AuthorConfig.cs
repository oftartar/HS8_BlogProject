using HS8_BlogProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HS8_BlogProject.Infrastructure.EntityTypeConfig
{
    internal class AuthorConfig : BaseEntityConfig<Author>
    {
        public override void Configure(EntityTypeBuilder<Author> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).IsRequired(true).HasMaxLength(30);
            builder.Property(x => x.LastName).IsRequired(true).HasMaxLength(30);
            builder.Property(x => x.ImagePath).IsRequired(true);

            builder.HasOne(x => x.AppUser).WithOne(x => x.Author).HasForeignKey<Author>(x => x.AppUserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
