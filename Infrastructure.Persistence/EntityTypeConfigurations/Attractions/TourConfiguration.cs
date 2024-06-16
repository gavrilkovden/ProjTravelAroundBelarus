using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Travels.Domain;

namespace Infrastructure.Persistence.EntityTypeConfigurations.Attractions
{
    public class TourConfiguration : IEntityTypeConfiguration<Tour>
    {
        public void Configure(EntityTypeBuilder<Tour> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).IsRequired().HasMaxLength(255);
            builder.Property(t => t.Description).IsRequired();
            builder.Property(a => a.Price).HasPrecision(18, 2);
            builder.Property(e => e.IsApproved).IsRequired().HasDefaultValue(false);

            builder.HasOne(t => t.Route)
                .WithMany(r => r.Tours)
                .HasForeignKey(t => t.RouteId);

            builder.HasMany(t => t.TourFeedback)
                .WithOne(c => c.Tour)
                .HasForeignKey(c => c.TourId);
        }
    }
}
