using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations
{
    public class VisitantConfigurations : IEntityTypeConfiguration<Visitant>
    {
        public void Configure(EntityTypeBuilder<Visitant> builder)
        {
            builder.Property(e => e.FullName).HasMaxLength(200);
            builder.Property(e => e.Phone).HasMaxLength(18);
            builder.Property(e => e.FrequentOtherChurch).HasDefaultValue(false);
            builder.Property(e => e.Companion).HasMaxLength(200);
        }
    }
}