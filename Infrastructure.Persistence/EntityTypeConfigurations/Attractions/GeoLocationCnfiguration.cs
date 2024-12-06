using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.Domain;

namespace Infrastructure.Persistence.EntityTypeConfigurations.Attractions
{
    public class GeoLocationCnfiguration : IEntityTypeConfiguration<GeoLocation>
    {
        public void Configure(EntityTypeBuilder<GeoLocation> builder)
        {
            builder.HasKey(gl => gl.Id);

            builder.Property(gl => gl.Latitude)
                   .HasMaxLength(50);

            builder.Property(gl => gl.Longitude)
                   .HasMaxLength(50);

            builder.HasOne(gl => gl.Attraction)
                   .WithOne(a => a.GeoLocation)
                   .HasForeignKey<GeoLocation>(gl => gl.AttractionId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
