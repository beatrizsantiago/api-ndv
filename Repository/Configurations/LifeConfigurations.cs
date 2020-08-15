using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations
{
    public class LifeConfigurations : IEntityTypeConfiguration<Life>
    {
        public void Configure(EntityTypeBuilder<Life> builder)
        {
            builder.Property(e => e.FullName).HasMaxLength(200);
            builder.Property(e => e.Email).HasMaxLength(200);
            builder.Property(e => e.Phone).HasMaxLength(18);
            builder.Property(e => e.IsLost).HasDefaultValue(false);
            builder.Property(e => e.BaptismOtherChurch).HasDefaultValue(false);
            builder.Property(e => e.BaptismMinister).HasMaxLength(200);
        }
    }
}