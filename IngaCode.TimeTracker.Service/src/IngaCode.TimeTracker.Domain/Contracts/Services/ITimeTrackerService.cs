using IngaCode.TimeTracker.Domain.Dtos.TimeTrackers;

namespace IngaCode.TimeTracker.Domain.Contracts.Services
{
    public interface ITimeTrackerService : IDisposable
    {
        Task<TimeTrackerDto> CreateTimeTrackerAsync(CreationTimeTrackerDto timeTracker, CancellationToken cancellationToken);

        Task DeleteTimeTrackerAsync(Guid timeTrackerId, CancellationToken cancellationToken);

        Task<TimeTrackerDto[]> GetTimeTrackersByQueryAsync(TimeTrackerFilterDto timeTrackerQuery, CancellationToken cancellationToken);

        Task UpdateEndDateTimeTrackerAsync(Guid timeTrackerId, UpdateTimeTrackerDto timeTracker, CancellationToken cancellationToken);

        Task UpdateTimeTrackerAsync(Guid timeTrackerId, UpdateTimeTrackerDto timeTracker, CancellationToken cancellationToken);
    }
}
