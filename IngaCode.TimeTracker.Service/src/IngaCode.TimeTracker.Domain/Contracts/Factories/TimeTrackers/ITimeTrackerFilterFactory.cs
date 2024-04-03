using IngaCode.TimeTracker.Domain.Contracts.Decorators.TimeTrackers;
using IngaCode.TimeTracker.Domain.Entities.TimeTrackers;
using IngaCode.TimeTracker.Domain.Queries.TimeTrackers;

namespace IngaCode.TimeTracker.Domain.Contracts.Factories.TimeTrackers
{
    public interface ITimeTrackerFilterFactory
    {
        public ITimeTrackerFilterDecorator Create
        (
            IQueryable<TimeTrackerEntity> timeTrackers,
            TimeTrackerQuery timeTrackerQuery
        );
    }
}
