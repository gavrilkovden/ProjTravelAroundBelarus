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
    public class AttractionFeedbackConfiguration : IEntityTypeConfiguration<AttractionFeedback>
    {
        public void Configure(EntityTypeBuilder<AttractionFeedback> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(a => a.ValueRating);


            builder.HasOne(r => r.Attraction)
                   .WithMany(a => a.AttractionFeedback)
                   .HasForeignKey(r => r.AttractionId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
