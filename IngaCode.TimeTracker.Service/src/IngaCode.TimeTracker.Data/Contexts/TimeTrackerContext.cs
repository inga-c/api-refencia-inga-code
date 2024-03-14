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
    }
}