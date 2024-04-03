using IngaCode.TimeTracker.Domain.Contracts.Decorators.TimeTrackers;
using IngaCode.TimeTracker.Domain.Entities.TimeTrackers;
using IngaCode.TimeTracker.Domain.Queries.TimeTrackers;

namespace IngaCode.TimeTracker.Data.Decorators.TimeTrackers
{
    public class StartDateFilterDecorator : ITimeTrackerFilterDecorator
    {
        private readonly ITimeTrackerFilterDecorator _innerDecorator;
        private readonly TimeTrackerQuery _timeTrackerQuery;

        public StartDateFilterDecorator
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

            if (_timeTrackerQuery.StartDate is not null)
            {
                return timeTrackerFiltered.Where(x => x.StartDate == _timeTrackerQuery.StartDate);
            }

            return timeTrackerFiltered;
        }
    }
}
