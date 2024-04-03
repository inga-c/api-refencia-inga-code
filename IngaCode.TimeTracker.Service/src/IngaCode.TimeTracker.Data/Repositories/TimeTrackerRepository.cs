using IngaCode.TimeTracker.Data.Contexts;
using IngaCode.TimeTracker.Domain.Contracts.Factories.TimeTrackers;
using IngaCode.TimeTracker.Domain.Contracts.Repositories;
using IngaCode.TimeTracker.Domain.Entities.TimeTrackers;
using IngaCode.TimeTracker.Domain.Queries.TimeTrackers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IngaCode.TimeTracker.Data.Repositories
{
    public class TimeTrackerRepository : ITimeTrackerRepository
    {
        private readonly ILogger<TimeTrackerRepository> _logger;
        private readonly TimeTrackerContext _context;
        private readonly ITimeTrackerFilterFactory _timeTrackerFilterFactory;

        public TimeTrackerRepository
        (
            ILogger<TimeTrackerRepository> logger,
            TimeTrackerContext context,
            ITimeTrackerFilterFactory timeTrackerFilterFactory
        )
        {
            _logger = logger;
            _context = context;
            _timeTrackerFilterFactory = timeTrackerFilterFactory;
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

        public async Task DeleteTimeTrackerAsync(TimeTrackerEntity timeTracker, CancellationToken cancellationToken)
        {
            try
            {
                _context.TimeTrackers.Remove(timeTracker);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Deu erro");
                throw;
            }
        }

        public async Task<TimeTrackerEntity?> GetTimeTrackerByIdAsync(Guid timeTrackerId, CancellationToken cancellationToken)
        {
            try
            {
                var query = new TimeTrackerQuery
                {
                    Id = timeTrackerId
                };

                var timeTrackers = await GetTimeTrackersAsync(query, cancellationToken);
                return timeTrackers.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Deu erro");
                throw;
            }
        }

        public Task<TimeTrackerEntity[]> GetTimeTrackersAsync(TimeTrackerQuery timeTrackerQuery, CancellationToken cancellationToken)
        {
            try
            {
                var queryable = _context.TimeTrackers.AsQueryable();
                var timeTrackerFiltered = _timeTrackerFilterFactory
                    .Create(queryable, timeTrackerQuery)
                    .Filter();

                return timeTrackerFiltered.ToArrayAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Deu erro");
                throw;
            }
        }

        public async Task UpdateTimeTrackerAsync(TimeTrackerEntity timeTracker, CancellationToken cancellationToken)
        {
            try
            {
                _context.TimeTrackers.Update(timeTracker);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Deu erro");
                throw;
            }
        }
    }
}