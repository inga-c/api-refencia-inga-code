using IngaCode.TimeTracker.Domain.Dtos.TimeTrackers;

namespace IngaCode.TimeTracker.Domain.Contracts.Services
{
    public interface ITimeTrackerService : IDisposable
    {
        Task<TimeTrackerDto> CreateTimeTracker(CreationTimeTrackerDto timeTracker);

        Task DeleteTimeTracker(Guid timeTrackerId);

        Task<TimeTrackerDto[]> GetTimeTrackersByQuery(TimeTrackerFilterDto timeTrackerQuery);

        Task UpdateEndDateTimeTracker(Guid timeTrackerId, UpdateTimeTrackerDto timeTracker);

        Task UpdateTimeTracker(Guid timeTrackerId, UpdateTimeTrackerDto timeTracker);
    }
}
