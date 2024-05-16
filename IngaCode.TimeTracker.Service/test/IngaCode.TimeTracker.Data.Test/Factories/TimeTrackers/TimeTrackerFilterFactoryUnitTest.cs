using IngaCode.TimeTracker.Data.Decorators.TimeTrackers;
using IngaCode.TimeTracker.Data.Factories.TimeTrackers;
using IngaCode.TimeTracker.Domain.Entities.TimeTrackers;
using IngaCode.TimeTracker.Domain.Queries.TimeTrackers;

namespace IngaCode.TimeTracker.Data.Test.Factories.TimeTrackers
{
    public class TimeTrackerFilterFactoryUnitTest
    {
        [Fact]
        public void Should_Return_Type_Decorator_Instance()
        {
            // Arrange
            var filterFactory = new TimeTrackerFilterFactory();
            var timeTrackers = new List<TimeTrackerEntity>().AsQueryable();
            var timeTrackerQuery = new TimeTrackerQuery
            {
                CollaboratorId = Guid.NewGuid(),
            };

            // Act
            var filterDecorator = filterFactory.Create(timeTrackers, timeTrackerQuery);

            // Assert
            Assert.IsType<WorkspaceIdFilterDecorator>(filterDecorator);
        }
    }
}
