using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Travels.Domain;

namespace Infrastructure.Persistence.EntityTypeConfigurations.Attractions
{
    public class TourFeedbackConfiguration : IEntityTypeConfiguration<TourFeedback>
    {
        public void Configure(EntityTypeBuilder<TourFeedback> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(a => a.Value);

            builder.HasOne(r => r.Tour)
                .WithMany(t => t.TourFeedback)
                .HasForeignKey(r => r.TourId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
