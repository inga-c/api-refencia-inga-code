using IngaCode.TimeTracker.Domain.Entities.TimeTrackers;
using IngaCode.TimeTracker.Domain.Queries.TimeTrackers;

namespace IngaCode.TimeTracker.Domain.Contracts.Repositories
{
    public interface ITimeTrackerRepository : IDisposable
    {
        Task<TimeTrackerEntity> CreateTimeTrackerAsync(TimeTrackerEntity timeTracker, CancellationToken cancellationToken);

        Task DeleteTimeTrackerAsync(TimeTrackerEntity timeTracker, CancellationToken cancellationToken);

        Task<TimeTrackerEntity?> GetTimeTrackerByIdAsync(Guid timeTrackerId, CancellationToken cancellationToken);

        Task<TimeTrackerEntity[]> GetTimeTrackersAsync(TimeTrackerQuery timeTrackerQuery, CancellationToken cancellationToken);

        Task UpdateTimeTrackerAsync(TimeTrackerEntity timeTracker, CancellationToken cancellationToken);
    }
}