using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Travels.Domain;

namespace Infrastructure.Persistence.EntityTypeConfigurations.Attractions
{
    public class WorkScheduleConfiguration : IEntityTypeConfiguration<WorkSchedule>
    {
        public void Configure(EntityTypeBuilder<WorkSchedule> builder)
        {
            builder.HasKey(ws => ws.Id);
            builder.Property(ws => ws.DayOfWeek).IsRequired();
            builder.Property(ws => ws.OpenTime).IsRequired();
            builder.Property(ws => ws.CloseTime).IsRequired();

            builder.HasOne(s => s.Attraction)
                .WithMany(a => a.WorkSchedules)
                .HasForeignKey(w => w.AttractionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
