using IngaCode.TimeTracker.Data.Contexts;
using IngaCode.TimeTracker.Domain.Contracts.Repositories;
using IngaCode.TimeTracker.Domain.Entities.TimeTrackers;
using IngaCode.TimeTracker.Domain.Queries.TimeTrackers;
using Microsoft.Extensions.Logging;

namespace IngaCode.TimeTracker.Data.Repositories
{
    public class TimeTrackerRepository : ITimeTrackerRepository
    {
        private readonly ILogger<TimeTrackerRepository> _logger;
        private readonly TimeTrackerContext _context;

        public TimeTrackerRepository
        (
            ILogger<TimeTrackerRepository> logger,
            TimeTrackerContext context
        )
        {
            _logger = logger;
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<TimeTrackerEntity> CreateTimeTrackerAsync
        (
            TimeTrackerEntity timeTracker,
            CancellationToken cancellationToken
        )
        {
            try
            {
                await _context.TimeTrackers.AddAsync(timeTracker, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return timeTracker;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Deu erro");
                throw;
            }
        }

        public Task DeleteTimeTracker(TimeTrackerEntity timeTracker)
        {
            throw new NotImplementedException();
        }

        public Task<TimeTrackerEntity?> GetTimeTrackerById(Guid timeTrackerId)
        {
            throw new NotImplementedException();
        }

        public Task<TimeTrackerEntity[]> GetTimeTrackers(TimeTrackerQuery timeTrackerQuery)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTimeTracker(TimeTrackerEntity timeTracker)
        {
            throw new NotImplementedException();
        }
    }
}