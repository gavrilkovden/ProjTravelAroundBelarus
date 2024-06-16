using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travels.Domain;

namespace Infrastructure.Persistence.EntityTypeConfigurations.Attractions;

public class AttractionConfiguration : IEntityTypeConfiguration<Attraction>
{
    public void Configure(EntityTypeBuilder<Attraction> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Name).IsRequired().HasMaxLength(255);
        builder.Property(a => a.Description).IsRequired();
        builder.Property(a => a.Price).HasPrecision(18, 2);
        builder.Property(a => a.NumberOfVisitors);
        builder.Property(e => e.IsApproved).IsRequired().HasDefaultValue(false);

        builder.HasMany(a => a.AttractionsInRoutes)
            .WithOne(i => i.Attraction)
            .HasForeignKey(s => s.AttractionId);
        builder.HasMany(a => a.AttractionFeedback).WithOne(c => c.Attraction);


        builder.HasMany(a => a.AttractionsInRoutes)
    .WithOne(i => i.Attraction)
    .HasForeignKey(s => s.AttractionId)
    .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(a => a.AttractionFeedback)
            .WithOne(c => c.Attraction)
            .HasForeignKey(c => c.AttractionId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasOne(a => a.User)
            .WithMany()
            .HasForeignKey(a => a.UserId);
    }
}
