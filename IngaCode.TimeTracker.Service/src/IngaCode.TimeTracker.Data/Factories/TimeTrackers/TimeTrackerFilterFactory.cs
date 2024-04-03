using IngaCode.TimeTracker.Data.Decorators.TimeTrackers;
using IngaCode.TimeTracker.Domain.Contracts.Decorators.TimeTrackers;
using IngaCode.TimeTracker.Domain.Contracts.Factories.TimeTrackers;
using IngaCode.TimeTracker.Domain.Entities.TimeTrackers;
using IngaCode.TimeTracker.Domain.Queries.TimeTrackers;

namespace IngaCode.TimeTracker.Data.Factories.TimeTrackers
{
    public class TimeTrackerFilterFactory : ITimeTrackerFilterFactory
    {
        public ITimeTrackerFilterDecorator Create
        (
            IQueryable<TimeTrackerEntity> timeTrackers,
            TimeTrackerQuery timeTrackerQuery
        )
        {
            ITimeTrackerFilterDecorator decorator = new BaseFilterDecorator(timeTrackers);
            decorator = new IdFilterDecorator(decorator, timeTrackerQuery);
            decorator = new StartDateFilterDecorator(decorator, timeTrackerQuery);
            decorator = new EndDateFilterDecorator(decorator, timeTrackerQuery);
            decorator = new TimeZoneIdFilterDecorator(decorator, timeTrackerQuery);
            decorator = new TaskIdFilterDecorator(decorator, timeTrackerQuery);
            decorator = new ProjectIdFilterDecorator(decorator, timeTrackerQuery);
            decorator = new CollaboratorIdFilterDecorator(decorator, timeTrackerQuery);
            decorator = new WorkspaceIdFilterDecorator(decorator, timeTrackerQuery);

            return decorator;
        }
    }
}
