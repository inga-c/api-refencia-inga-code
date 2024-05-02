using IngaCode.TimeTracker.Data.Decorators.TimeTrackers;
using IngaCode.TimeTracker.Domain.Entities.TimeTrackers;

namespace IngaCode.TimeTracker.Data.Test.Decorators.TimeTrackers
{
    public class BaseFilterDecoratorUnitTest
    {
        [Fact]
        public void Should_Return_Queryable_Non_Empty_Time_Tracker_When_Calling_Filter_With_Value() 
        {
            // Arrange
            var timeTrackers = new List<TimeTrackerEntity> { new() }.AsQueryable();
            var baseFilterDecorator = new BaseFilterDecorator(timeTrackers);

            // Act
            var timeTrackersFiltered = baseFilterDecorator.Filter();

            // Assert
            Assert.NotNull(timeTrackersFiltered);
            Assert.NotEmpty(timeTrackersFiltered);
        }

        [Fact]
        public void Should_Return_Queryable_Empty_Time_Tracker_When_Calling_Filter_Without_Value()
        {
            // Arrange
            var timeTrackers = new List<TimeTrackerEntity>().AsQueryable();
            var baseFilterDecorator = new BaseFilterDecorator(timeTrackers);

            // Act
            var timeTrackersFiltered = baseFilterDecorator.Filter();

            // Assert
            Assert.NotNull(timeTrackersFiltered);
            Assert.Empty(timeTrackersFiltered);
        }
    }
}
