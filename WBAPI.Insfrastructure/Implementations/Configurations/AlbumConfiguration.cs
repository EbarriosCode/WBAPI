using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WBAPI.Domain.Entities;

namespace WBAPI.Infrastructure.Implementations.Configurations
{
    public class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.Artist)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(a => a.Genre)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(a => a.Year)
                .IsRequired();

            builder.Property(a => a.CreatedAt)
                .IsRequired();

            builder.Property(a => a.UpdatedAt)
                .IsRequired(false);
          
            builder.HasQueryFilter(a => a.IsActive);

            // Index for frequently searchs by artist
            builder.HasIndex(a => a.Artist)
                .HasDatabaseName("IX_Albums_Artist");

            builder.ToTable("Albums");
        }
    }
}
