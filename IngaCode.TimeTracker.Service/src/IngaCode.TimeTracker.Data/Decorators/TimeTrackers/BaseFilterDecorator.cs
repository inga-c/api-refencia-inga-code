using IngaCode.TimeTracker.Domain.Contracts.Decorators.TimeTrackers;
using IngaCode.TimeTracker.Domain.Entities.TimeTrackers;

namespace IngaCode.TimeTracker.Data.Decorators.TimeTrackers
{
    public class BaseFilterDecorator : ITimeTrackerFilterDecorator
    {
        private readonly IQueryable<TimeTrackerEntity> _timeTrackers;

        public BaseFilterDecorator(IQueryable<TimeTrackerEntity> timeTrackers)
        {
            _timeTrackers = timeTrackers;
        }

        public IQueryable<TimeTrackerEntity> Filter() => _timeTrackers;
    }
}
