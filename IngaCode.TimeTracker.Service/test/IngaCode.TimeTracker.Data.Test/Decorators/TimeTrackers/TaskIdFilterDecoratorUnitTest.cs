using FluentAssertions;
using IngaCode.TimeTracker.Data.Decorators.TimeTrackers;
using IngaCode.TimeTracker.Domain.Contracts.Decorators.TimeTrackers;
using IngaCode.TimeTracker.Domain.Entities.TimeTrackers;
using IngaCode.TimeTracker.Domain.Queries.TimeTrackers;
using Moq;

namespace IngaCode.TimeTracker.Data.Test.Decorators.TimeTrackers
{
    public class TaskIdFilterDecoratorUnitTestUnitTest
    {
        private readonly Mock<ITimeTrackerFilterDecorator> _timeTrackerFilterDecorator;

        public TaskIdFilterDecoratorUnitTestUnitTest()
        {
            _timeTrackerFilterDecorator = new Mock<ITimeTrackerFilterDecorator>();
        }

        [Fact]
        public void Should_Return_Original_Queryable_Time_Tracker_When_Calling_Filter_Without_Parameters()
        {
            // Arrange
            var timeTrackerQuery = new TimeTrackerQuery
            {
                TaskId = null,
            };
            var timeTrackers = new List<TimeTrackerEntity> { new() }.AsQueryable();

            _timeTrackerFilterDecorator
                .Setup(x => x.Filter())
                .Returns(timeTrackers);

            var filterDecorator = new TaskIdFilterDecorator
            (
                _timeTrackerFilterDecorator.Object,
                timeTrackerQuery
            );

            // Act
            var timeTrackersFiltered = filterDecorator.Filter();

            // Assert
            Assert.NotNull(timeTrackersFiltered);
            Assert.NotEmpty(timeTrackersFiltered);

            timeTrackersFiltered
                .Should()
                .BeEquivalentTo(timeTrackers.AsQueryable());
        }

        [Fact]
        public void Should_Return_Filtered_Queryable_Time_Tracker_When_Calling_Filter_With_Parameters()
        {
            // Arrange
            var timeTrackerQuery = new TimeTrackerQuery
            {
                TaskId = Guid.NewGuid(),
            };
            var timeTrackers = new List<TimeTrackerEntity>
            {
                new()
                {
                    TaskId = timeTrackerQuery.TaskId.Value,
                },
                new()
                {
                    TaskId = Guid.NewGuid(),
                }
            }.AsQueryable();

            _timeTrackerFilterDecorator
                .Setup(x => x.Filter())
                .Returns(timeTrackers);

            var filterDecorator = new TaskIdFilterDecorator
            (
                _timeTrackerFilterDecorator.Object,
                timeTrackerQuery
            );

            // Act
            var timeTrackersFiltered = filterDecorator.Filter();

            // Assert
            Assert.NotNull(timeTrackersFiltered);
            Assert.NotEmpty(timeTrackersFiltered);
            Assert.Single(timeTrackersFiltered);

            timeTrackersFiltered
                .Should()
                .BeEquivalentTo(new List<TimeTrackerEntity>
                {
                    new()
                    {
                        TaskId = timeTrackerQuery.TaskId.Value,
                    },
                }.AsQueryable());
        }
    }
}
