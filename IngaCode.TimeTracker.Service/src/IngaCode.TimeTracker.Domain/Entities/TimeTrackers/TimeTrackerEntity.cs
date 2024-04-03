using IngaCode.TimeTracker.Domain.Entities.Commons;

namespace IngaCode.TimeTracker.Domain.Entities.TimeTrackers
{
    public class TimeTrackerEntity : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string TimeZoneId { get; set; }
        public Guid? TaskId { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid CollaboratorId { get; set; }
        public Guid WorkspaceId { get; set; }
    }
}