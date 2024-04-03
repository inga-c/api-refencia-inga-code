using IngaCode.TimeTracker.Domain.Contracts.Decorators.TimeTrackers;
using IngaCode.TimeTracker.Domain.Entities.TimeTrackers;
using IngaCode.TimeTracker.Domain.Queries.TimeTrackers;

namespace IngaCode.TimeTracker.Data.Decorators.TimeTrackers
{
    public class IdFilterDecorator : ITimeTrackerFilterDecorator
    {
        private readonly ITimeTrackerFilterDecorator _innerDecorator;
        private readonly TimeTrackerQuery _timeTrackerQuery;

        public IdFilterDecorator
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

            if (_timeTrackerQuery.Id is not null)
            {
                return timeTrackerFiltered.Where(x => x.Id == _timeTrackerQuery.Id);
            }

            return timeTrackerFiltered;
        }
    }
}
