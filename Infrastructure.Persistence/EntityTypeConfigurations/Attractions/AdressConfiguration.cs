using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.Domain;

namespace Infrastructure.Persistence.EntityTypeConfigurations.Attractions
{
    public class AdressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Street).HasMaxLength(50);
            builder.Property(a => a.City).HasMaxLength(50);
            builder.Property(a => a.Region).IsRequired().HasMaxLength(50);

            builder.HasMany(a => a.Attractions)
                      .WithOne(attraction => attraction.Address)
                      .HasForeignKey(attraction => attraction.AddressId);
        }
    }
}
