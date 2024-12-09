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
    public class ImageConfiration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.ImagePath).IsRequired().HasMaxLength(500);
            builder.Property(e => e.IsApproved).IsRequired().HasDefaultValue(false);
            builder.Property(e => e.IsCover).IsRequired().HasDefaultValue(false);
            builder.Property(e => e.UserId).IsRequired();

            builder.HasOne(i => i.Attraction)
                   .WithMany(a => a.Images)
                   .HasForeignKey(i => i.AttractionId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.User) 
                .WithMany()
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(i => i.UserId); // Добавление индекса на UserId
        }
    }
}
