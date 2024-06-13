using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using IngaCode.TimeTracker.Application.Exceptions;
using IngaCode.TimeTracker.Application.Services;
using IngaCode.TimeTracker.Domain.Contracts.Repositories;
using IngaCode.TimeTracker.Domain.Dtos.TimeTrackers;
using IngaCode.TimeTracker.Domain.Entities.TimeTrackers;
using Microsoft.Extensions.Logging;
using Moq;

namespace IngaCode.TimeTracker.Application.Test.Services
{
    public class TimeTrackerServiceUnitTest
    {
        private readonly TimeTrackerService _timeTrackerService;
        private readonly Mock<ILogger<TimeTrackerService>> _logger;
        private readonly Mock<ITimeTrackerRepository> _timeTrackerRepository;
        private readonly Mock<IValidator<CreationTimeTrackerDto>> _creationTimeTrackerValidator;
        private readonly Mock<IValidator<UpdateTimeTrackerDto>> _updateTimeTrackerValidator;

        public TimeTrackerServiceUnitTest()
        {
            _logger = new Mock<ILogger<TimeTrackerService>>();
            _timeTrackerRepository = new Mock<ITimeTrackerRepository>();
            _creationTimeTrackerValidator = new Mock<IValidator<CreationTimeTrackerDto>>();
            _updateTimeTrackerValidator = new Mock<IValidator<UpdateTimeTrackerDto>>();

            _timeTrackerService = new TimeTrackerService
            (
                _logger.Object,
                _timeTrackerRepository.Object,
                _creationTimeTrackerValidator.Object,
                _updateTimeTrackerValidator.Object
            );
        }

        [Fact]
        public async void ShouldCreateTimeTrackerWhenTimeTrackerIsValid()
        {
            // Arrange
            var timeTracker = new CreationTimeTrackerDto
            {
                ProjectId = Guid.NewGuid(),
                StartTime = new TimeSpan(10, 00, 00),
                EndTime = new TimeSpan(12, 00, 00),
                TaskId = Guid.NewGuid(),
                TimeZoneId = "America/Sao_Paulo",
            };

            var timeTrackerCreated = new TimeTrackerEntity
            {
                Id = Guid.NewGuid(),
                CollaboratorId = Guid.NewGuid(),
                ProjectId = timeTracker.ProjectId,
                StartDate = new DateTime(2023, 1, 1, 13, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2023, 1, 1, 15, 0, 0, DateTimeKind.Utc),
                TaskId = timeTracker.TaskId,
                TimeZoneId = timeTracker.TimeZoneId
            };

            _timeTrackerRepository
                .Setup(x => x.CreateTimeTrackerAsync(It.IsAny<TimeTrackerEntity>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(timeTrackerCreated);

            // Act
            var result = await _timeTrackerService.CreateTimeTrackerAsync(timeTracker, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(new TimeTrackerDto
            {
                Id = timeTrackerCreated.Id,
                ProjectId = timeTracker.ProjectId,
                CollaboratorId = timeTrackerCreated.CollaboratorId,
                EndDate = timeTrackerCreated.EndDate,
                EndTime = timeTracker.EndTime,
                StartDate = timeTrackerCreated.StartDate,
                StartTime = timeTracker.StartTime,
                TaskId = timeTrackerCreated.TaskId,
                TimeZoneId = timeTrackerCreated.TimeZoneId
            });
        }

        [Fact]
        public async void ShouldThrowExceptionWhenTimeTrackerIsInvalid()
        {
            // Arrange
            var timeTracker = new CreationTimeTrackerDto
            {
                ProjectId = Guid.NewGuid(),
                StartTime = new TimeSpan(10, 00, 00),
                EndTime = new TimeSpan(12, 00, 00),
                TaskId = Guid.NewGuid(),
                TimeZoneId = "America/Sao_Paulo",
            };

            var errors = new List<ValidationFailure>
            {
                new("SomeProperty", "SomeError")
                {
                    CustomState = "CustomState",
                    ErrorCode = "ErrorCode",
                    FormattedMessagePlaceholderValues = []
                }
            };

            var validationException = new ValidationException(errors);

            _creationTimeTrackerValidator
                .Setup(x => x.Validate(It.IsAny<ValidationContext<CreationTimeTrackerDto>>()))
                .Throws(validationException);

            // Act
            var exception = await Assert.ThrowsAsync<DtoValidationException>
            (
                () => _timeTrackerService.CreateTimeTrackerAsync(timeTracker, CancellationToken.None)
            );

            // Assert
            Assert.NotNull(exception.ValidationErrors);
            Assert.Contains(exception.ValidationErrors, x => x.Message == "SomeError");
            Assert.Contains(exception.ValidationErrors, x => x.Code == "ErrorCode");
            Assert.Contains(exception.ValidationErrors, x => x.PropertyName == "SomeProperty");
            Assert.Contains(exception.ValidationErrors, x => x.Value.Equals("CustomState"));
        }

        [Fact]
        public async void ShouldThrowExceptionWhenCreateTimeTrackerFailure()
        {
            // Arrange
            var timeTracker = new CreationTimeTrackerDto
            {
                ProjectId = Guid.NewGuid(),
                StartTime = new TimeSpan(10, 00, 00),
                EndTime = new TimeSpan(12, 00, 00),
                TaskId = Guid.NewGuid(),
                TimeZoneId = "America/Sao_Paulo",
            };

            _timeTrackerRepository
                .Setup(x => x.CreateTimeTrackerAsync(It.IsAny<TimeTrackerEntity>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception("Any Error."));

            // Act
            var createTimeTrackerAction = () => _timeTrackerService.CreateTimeTrackerAsync(timeTracker, CancellationToken.None);

            // Assert
            var exception = await Assert.ThrowsAsync<TimeTrackerException>(createTimeTrackerAction);
            Assert.NotNull(exception.SystemMessage);
            Assert.NotNull(exception.CodeMessage);
            Assert.Equal("ERR001", exception.CodeMessage);

            _logger.Verify(logger => logger.Log(
                It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((@object, @type) => true),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once());
        }
    }
}