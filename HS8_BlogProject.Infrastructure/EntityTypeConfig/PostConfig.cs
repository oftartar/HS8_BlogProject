using HS8_BlogProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HS8_BlogProject.Infrastructure.EntityTypeConfig
{
    internal class PostConfig : BaseEntityConfig<Post>
    {
        public override void Configure(EntityTypeBuilder<Post> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).IsRequired(true);
            builder.Property(x => x.Content).IsRequired(true);
            builder.Property(x => x.ImagePath).IsRequired(true);

            builder.HasOne(x => x.Author).WithMany(x => x.Posts).HasForeignKey(x => x.AuthorId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Genre).WithMany(x => x.Posts).HasForeignKey(x => x.GenreId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
