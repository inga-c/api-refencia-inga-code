using IngaCode.TimeTracker.Data.Contexts;
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

                if (timeTrackerQuery.Id is not null)
                {
                    queryable = queryable.Where(x => x.Id == timeTrackerQuery.Id);
                }

                if (timeTrackerQuery.StartDate is not null)
                {
                    queryable = queryable.Where(x => x.StartDate == timeTrackerQuery.StartDate);
                }

                if (timeTrackerQuery.EndDate is not null)
                {
                    queryable = queryable.Where(x => x.StartDate == timeTrackerQuery.EndDate);
                }

                if (timeTrackerQuery.TimeZoneId is not null)
                {
                    queryable = queryable.Where(x => x.TimeZoneId == timeTrackerQuery.TimeZoneId);
                }

                if (timeTrackerQuery.TaskId is not null)
                {
                    queryable = queryable.Where(x => x.TaskId == timeTrackerQuery.TaskId);
                }

                if (timeTrackerQuery.ProjectId is not null)
                {
                    queryable = queryable.Where(x => x.ProjectId == timeTrackerQuery.ProjectId);
                }

                if (timeTrackerQuery.CollaboratorId is not null)
                {
                    queryable = queryable.Where(x => x.CollaboratorId == timeTrackerQuery.CollaboratorId);
                }

                if (timeTrackerQuery.WorkspaceId is not null)
                {
                    queryable = queryable.Where(x => x.WorkspaceId == timeTrackerQuery.WorkspaceId);
                }

                return queryable.ToArrayAsync(cancellationToken);
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