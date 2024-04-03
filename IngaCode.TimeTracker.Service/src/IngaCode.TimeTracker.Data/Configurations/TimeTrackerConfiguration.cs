using IngaCode.TimeTracker.Domain.Entities.TimeTrackers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IngaCode.TimeTracker.Data.Configurations
{
    public class TimeTrackerConfiguration : IEntityTypeConfiguration<TimeTrackerEntity>
    {
        public void Configure(EntityTypeBuilder<TimeTrackerEntity> builder)
        {
            builder.ToTable("TimeTracker");

            builder.HasKey(t => t.Id);
            builder.Property(t => t.StartDate).IsRequired();
            builder.Property(t => t.EndDate);
            builder.Property(t => t.TimeZoneId).IsRequired().HasMaxLength(250);
            builder.Property(t => t.TaskId);
            builder.Property(t => t.ProjectId);
            builder.Property(t => t.CollaboratorId).IsRequired();
            builder.Property(t => t.WorkspaceId).IsRequired();

            builder.Property(t => t.CreatedAt).IsRequired();
            builder.Property(t => t.UpdatedAt);
            builder.Property(t => t.DeletedAt);
        }
    }
}
