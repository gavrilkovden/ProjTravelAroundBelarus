using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Travels.Domain;

namespace Infrastructure.Persistence.EntityTypeConfigurations.Attractions
{
    public class RouteConfiguration : IEntityTypeConfiguration<Route>
    {
        public void Configure(EntityTypeBuilder<Route> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Name).IsRequired().HasMaxLength(180);
            builder.Property(r => r.Description).IsRequired();


            builder.HasMany(r => r.AttractionsInRoutes)
                .WithOne(a => a.Route)
                .HasForeignKey(ar => ar.RouteId);

            builder.HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId);

            builder.HasMany(r => r.Tours)
                .WithOne(t => t.Route)
                .HasForeignKey(t => t.RouteId);
        }
    }
}
