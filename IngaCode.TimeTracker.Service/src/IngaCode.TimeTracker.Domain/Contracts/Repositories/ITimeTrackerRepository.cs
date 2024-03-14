using IngaCode.TimeTracker.Domain.Entities.TimeTrackers;
using IngaCode.TimeTracker.Domain.Queries.TimeTrackers;

namespace IngaCode.TimeTracker.Domain.Contracts.Repositories
{
    public interface ITimeTrackerRepository : IDisposable
    {
        Task<TimeTrackerEntity> CreateTimeTrackerAsync(TimeTrackerEntity timeTracker, CancellationToken cancellationToken);

        Task DeleteTimeTracker(TimeTrackerEntity timeTracker);

        Task<TimeTrackerEntity?> GetTimeTrackerById(Guid timeTrackerId);

        Task<TimeTrackerEntity[]> GetTimeTrackers(TimeTrackerQuery timeTrackerQuery);

        Task UpdateTimeTracker(TimeTrackerEntity timeTracker);
    }
}