using IngaCode.TimeTracker.Domain.Contracts.Decorators.TimeTrackers;
using IngaCode.TimeTracker.Domain.Entities.TimeTrackers;
using IngaCode.TimeTracker.Domain.Queries.TimeTrackers;

namespace IngaCode.TimeTracker.Data.Decorators.TimeTrackers
{
    public class CollaboratorIdFilterDecorator : ITimeTrackerFilterDecorator
    {
        private readonly ITimeTrackerFilterDecorator _innerDecorator;
        private readonly TimeTrackerQuery _timeTrackerQuery;

        public CollaboratorIdFilterDecorator
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

            if (_timeTrackerQuery.CollaboratorId is not null)
            {
                return timeTrackerFiltered.Where(x => x.CollaboratorId == _timeTrackerQuery.CollaboratorId);
            }

            return timeTrackerFiltered;
        }
    }
}
