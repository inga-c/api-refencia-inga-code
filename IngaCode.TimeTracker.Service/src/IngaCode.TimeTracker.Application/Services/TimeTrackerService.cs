using IngaCode.TimeTracker.Domain.Contracts.Repositories;
using IngaCode.TimeTracker.Domain.Contracts.Services;
using IngaCode.TimeTracker.Domain.Dtos.TimeTrackers;
using IngaCode.TimeTracker.Domain.Resources;
using Microsoft.Extensions.Logging;

namespace IngaCode.TimeTracker.Application.Services
{
    public class TimeTrackerService
    (
        ILogger<TimeTrackerService> logger,
        ITimeTrackerRepository repository
    ) : ITimeTrackerService
    {
        public void Dispose() => repository.Dispose();

        public Task<TimeTrackerDto> CreateTimeTracker(CreationTimeTrackerDto timeTracker)
        {
            try
            {

            }
            catch (Exception ex)
            {
                logger.LogError(ex, SystemMessage.ERR001);
                throw;
            }
        }

        public Task DeleteTimeTracker(Guid timeTrackerId)
        {
            throw new NotImplementedException();
        }

        public Task<TimeTrackerDto[]> GetTimeTrackersByQuery(TimeTrackerFilterDto timeTrackerQuery)
        {
            throw new NotImplementedException();
        }

        public Task UpdateEndDateTimeTracker(Guid timeTrackerId, UpdateTimeTrackerDto timeTracker)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTimeTracker(Guid timeTrackerId, UpdateTimeTrackerDto timeTracker)
        {
            throw new NotImplementedException();
        }
    }
}
