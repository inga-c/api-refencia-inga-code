using IngaCode.TimeTracker.Data.Configurations;
using IngaCode.TimeTracker.Domain.Entities.TimeTrackers;
using Microsoft.EntityFrameworkCore;

namespace IngaCode.TimeTracker.Data.Contexts
{
    public class TimeTrackerContext : DbContext
    {
        public TimeTrackerContext()
        { }

        public TimeTrackerContext(DbContextOptions<TimeTrackerContext> options) : base(options)
        { }

        public DbSet<TimeTrackerEntity> TimeTrackers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new TimeTrackerConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}