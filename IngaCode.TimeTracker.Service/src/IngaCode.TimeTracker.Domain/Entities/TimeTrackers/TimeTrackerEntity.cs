namespace IngaCode.TimeTracker.Domain.Entities.TimeTrackers
{
    public class TimeTrackerEntity
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string TimeZoneId { get; set; }
        public Guid? TaskId { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid CollaboratorId { get; set; }
        public Guid WorkspaceId { get; set; }
    }
}