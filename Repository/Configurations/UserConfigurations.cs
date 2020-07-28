using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.Name).HasMaxLength(200);
            builder.Property(e => e.IsEnabled).HasDefaultValue(true);
            builder.Property(e => e.Mentor).HasMaxLength(200);
        }
    }
}