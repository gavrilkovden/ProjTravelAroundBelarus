using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Travels.Domain;

namespace Infrastructure.Persistence.EntityTypeConfigurations.Attractions
{
    public class AttractionInRouteConfiguration : IEntityTypeConfiguration<AttractionInRoute>
    {
        public void Configure(EntityTypeBuilder<AttractionInRoute> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Order).IsRequired();
            builder.Property(a => a.DistanceToNextAttraction).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(a => a.VisitDateTime).IsRequired();

            builder.HasOne(a => a.Route)
                .WithMany(r => r.AttractionsInRoutes)
                .HasForeignKey(a => a.RouteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Attraction)
                .WithMany(r => r.AttractionsInRoutes)
                .HasForeignKey(a => a.AttractionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
