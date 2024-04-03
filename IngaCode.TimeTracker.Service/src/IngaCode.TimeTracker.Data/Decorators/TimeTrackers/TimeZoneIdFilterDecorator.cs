using IngaCode.TimeTracker.Domain.Contracts.Decorators.TimeTrackers;
using IngaCode.TimeTracker.Domain.Entities.TimeTrackers;
using IngaCode.TimeTracker.Domain.Queries.TimeTrackers;

namespace IngaCode.TimeTracker.Data.Decorators.TimeTrackers
{
    public class TimeZoneIdFilterDecorator : ITimeTrackerFilterDecorator
    {
        private readonly ITimeTrackerFilterDecorator _innerDecorator;
        private readonly TimeTrackerQuery _timeTrackerQuery;

        public TimeZoneIdFilterDecorator
        (
            ITimeTrackerFilterDecorator innerDecorator,
            TimeTrackerQuery timeTrackerQuery
        )
        {
            _innerDecorator = innerDecorator;
            _timeTrackerQuery = timeTrackerQuery;
        }

        public IQueryable<TimeTrackerEntity> Filter()
        {
            var timeTrackerFiltered = _innerDecorator.Filter();

            if (_timeTrackerQuery.TimeZoneId is not null)
            {
                return timeTrackerFiltered.Where(x => x.TimeZoneId == _timeTrackerQuery.TimeZoneId);
            }

            return timeTrackerFiltered;
        }
    }
}
