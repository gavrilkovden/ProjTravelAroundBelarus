using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travels.Domain;

namespace Infrastructure.Persistence.EntityTypeConfigurations.Attractions
{
    public class AdressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Street).HasMaxLength(50);
            builder.Property(a => a.City).IsRequired().HasMaxLength(50);
            builder.Property(a => a.Region).IsRequired().HasMaxLength(50);

            builder.Property(a => a.Region).HasConversion(
        v => v.ToString(),
        v => (RegionEnum)Enum.Parse(typeof(RegionEnum), v)).IsRequired();

            builder.Property(a => a.City).HasConversion(
                v => v.ToString(),
                v => (CityEnum)Enum.Parse(typeof(CityEnum), v)).IsRequired();
        }
    }
}
