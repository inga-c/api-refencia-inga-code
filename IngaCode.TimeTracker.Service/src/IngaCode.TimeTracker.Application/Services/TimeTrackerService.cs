using FluentValidation;
using IngaCode.TimeTracker.Application.Exceptions;
using IngaCode.TimeTracker.Domain.Contracts.Repositories;
using IngaCode.TimeTracker.Domain.Contracts.Services;
using IngaCode.TimeTracker.Domain.Dtos.TimeTrackers;
using IngaCode.TimeTracker.Domain.Entities.TimeTrackers;
using IngaCode.TimeTracker.Domain.Resources;
using Microsoft.Extensions.Logging;
using TimeZoneConverter;

namespace IngaCode.TimeTracker.Application.Services
{
    public class TimeTrackerService
    (
        ILogger<TimeTrackerService> logger,
        ITimeTrackerRepository repository,
        IValidator<CreationTimeTrackerDto> createTimeTrackerValidator
    ) : ITimeTrackerService
    {
        public void Dispose()
        {
            repository.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<TimeTrackerDto> CreateTimeTrackerAsync(CreationTimeTrackerDto timeTracker, CancellationToken cancellationToken)
        {
            try
            {
                createTimeTrackerValidator.ValidateAndThrow(timeTracker);

                var timeTrackerEntity = new TimeTrackerEntity
                {
                    Id = Guid.NewGuid(),
                    ProjectId = timeTracker.ProjectId,
                    StartDate = ToDateTimeFromTimeSpan(timeTracker.StartTime, timeTracker.TimeZoneId) ?? DateTime.UtcNow,
                    EndDate = ToDateTimeFromTimeSpan(timeTracker.EndTime, timeTracker.TimeZoneId),
                    TaskId = timeTracker.TaskId,
                    TimeZoneId = timeTracker.TimeZoneId
                };

                var timeTrackerCreated = await repository.CreateTimeTrackerAsync(timeTrackerEntity, cancellationToken);

                return new TimeTrackerDto
                {
                    Id = timeTrackerCreated.Id,
                    CollaboratorId = timeTrackerCreated.CollaboratorId,
                    ProjectId = timeTrackerCreated.ProjectId,
                    StartDate = timeTrackerCreated.StartDate,
                    EndDate = timeTrackerCreated.EndDate,
                    EndTime = ToTimeSpanFromDateTime(timeTrackerCreated.EndDate, timeTrackerCreated.TimeZoneId),
                    StartTime = ToTimeSpanFromDateTime(timeTrackerCreated.StartDate, timeTrackerCreated.TimeZoneId) ?? new TimeSpan(),
                    TaskId = timeTrackerCreated.TaskId,
                    TimeZoneId = timeTrackerCreated.TimeZoneId
                };
            }
            catch (ValidationException ex)
            {
                throw new DtoValidationException(ex);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, SystemMessage.ERR001);
                throw new TimeTrackerException(ex, SystemMessage.ERR001, nameof(SystemMessage.ERR001));
            }
        }

        public Task DeleteTimeTrackerAsync(Guid timeTrackerId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<TimeTrackerDto[]> GetTimeTrackersByQueryAsync(TimeTrackerFilterDto timeTrackerQuery, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateEndDateTimeTrackerAsync(Guid timeTrackerId, UpdateTimeTrackerDto timeTracker, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTimeTrackerAsync(Guid timeTrackerId, UpdateTimeTrackerDto timeTracker, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private static DateTime? ToDateTimeFromTimeSpan(TimeSpan? timeSpan, string timeZoneId)
        {
            if (timeSpan != null)
            {
                var timeZone = TZConvert.GetTimeZoneInfo(timeZoneId);
                var userDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, timeZone.Id);
                var today = new DateTime(userDate.Date.Ticks, DateTimeKind.Unspecified).Add(timeSpan.Value);
                var todayUTC = TimeZoneInfo.ConvertTimeToUtc(today, timeZone);
                return todayUTC;
            }

            return null;
        }

        private static TimeSpan? ToTimeSpanFromDateTime(DateTime? date, string timeZoneId)
        {
            if (date != null)
            {
                var timeZone = TZConvert.GetTimeZoneInfo(timeZoneId);
                var userDate = TimeZoneInfo.ConvertTimeFromUtc(date.Value, timeZone);

                return new TimeSpan(userDate.Hour, userDate.Minute, userDate.Second);
            }

            return null;
        }
    }
}