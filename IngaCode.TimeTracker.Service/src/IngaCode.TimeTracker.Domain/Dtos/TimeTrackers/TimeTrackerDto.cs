namespace IngaCode.TimeTracker.Domain.Dtos.TimeTrackers
{
    public class TimeTrackerDto
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string TimeZoneId { get; set; }
        public Guid? TaskId { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid CollaboratorId { get; set; }
    }
}
