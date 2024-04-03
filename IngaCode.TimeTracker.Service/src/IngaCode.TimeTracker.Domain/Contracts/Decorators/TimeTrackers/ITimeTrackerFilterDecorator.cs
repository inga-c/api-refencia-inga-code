using IngaCode.TimeTracker.Domain.Entities.TimeTrackers;

namespace IngaCode.TimeTracker.Domain.Contracts.Decorators.TimeTrackers
{
    public interface ITimeTrackerFilterDecorator
    {
        IQueryable<TimeTrackerEntity> Filter();
    }
}
