using FluentValidation;
using IngaCode.TimeTracker.Application.Exceptions;
using IngaCode.TimeTracker.Domain.Contracts.Repositories;
using IngaCode.TimeTracker.Domain.Contracts.Services;
using IngaCode.TimeTracker.Domain.Dtos.TimeTrackers;
using IngaCode.TimeTracker.Domain.Entities.TimeTrackers;
using IngaCode.TimeTracker.Domain.Queries.TimeTrackers;
using IngaCode.TimeTracker.Domain.Resources;
using Microsoft.Extensions.Logging;
using TimeZoneConverter;

namespace IngaCode.TimeTracker.Application.Services
{
    public class TimeTrackerService
    (
        ILogger<TimeTrackerService> logger,
        ITimeTrackerRepository repository,
        IValidator<CreationTimeTrackerDto> createTimeTrackerValidator,
        IValidator<UpdateTimeTrackerDto> updateTimeTrackerValidator
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

        public async Task DeleteTimeTrackerAsync(Guid timeTrackerId, CancellationToken cancellationToken)
        {
            try
            {
                var timeTrackerEntity = await repository.GetTimeTrackerByIdAsync(timeTrackerId, cancellationToken)
                    ?? throw new TimeTrackerException(string.Format(SystemMessage.ERR003, timeTrackerId), nameof(SystemMessage.ERR003));

                await repository.DeleteTimeTrackerAsync(timeTrackerEntity, cancellationToken);
            }
            catch (TimeTrackerException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, SystemMessage.ERR002);
                throw new TimeTrackerException(ex, SystemMessage.ERR002, nameof(SystemMessage.ERR002));
            }
        }

        public async Task<TimeTrackerDto[]> GetTimeTrackersByQueryAsync(TimeTrackerFilterDto timeTrackerQuery, CancellationToken cancellationToken)
        {
            try
            {
                var timeTrackerEntityQuery = new TimeTrackerQuery
                {
                    StartDate = timeTrackerQuery.StartDate,
                    TimeZoneId = timeTrackerQuery.TimeZoneId,
                };

                var timeTrackerEntities = await repository.GetTimeTrackersAsync(timeTrackerEntityQuery, cancellationToken);

                if (timeTrackerEntities.Length != 0)
                {
                    return [.. timeTrackerEntities.Select(p => new TimeTrackerDto
                    {
                        Id = p.Id,
                        CollaboratorId = p.CollaboratorId,
                        EndDate = p.EndDate,
                        ProjectId = p.ProjectId,
                        StartDate = p.StartDate,
                        EndTime = ToTimeSpanFromDateTime(p.EndDate, p.TimeZoneId),
                        StartTime = ToTimeSpanFromDateTime(p.StartDate, p.TimeZoneId) ?? new TimeSpan(),
                        TaskId = p.TaskId,
                        TimeZoneId = p.TimeZoneId
                    })
                    .OrderBy(x => x.StartDate)];
                }

                return [];
            }
            catch (Exception ex)
            {
                logger.LogError(ex, SystemMessage.ERR004);
                throw new TimeTrackerException(ex, SystemMessage.ERR004, nameof(SystemMessage.ERR004));
            }
        }

        public async Task UpdateEndDateTimeTrackerAsync(Guid timeTrackerId, UpdateTimeTrackerDto timeTracker, CancellationToken cancellationToken)
        {
            try
            {
                var timeTrackerEntity = await repository.GetTimeTrackerByIdAsync(timeTrackerId, cancellationToken)
                    ?? throw new TimeTrackerException(string.Format(SystemMessage.ERR004, timeTrackerId), nameof(SystemMessage.ERR004));

                timeTrackerEntity.EndDate = ToDateTimeFromTimeSpan(timeTracker.EndTime, timeTracker.TimeZoneId);

                await repository.UpdateTimeTrackerAsync(timeTrackerEntity, cancellationToken);
            }
            catch (TimeTrackerException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, SystemMessage.ERR005);
                throw new TimeTrackerException(ex, SystemMessage.ERR005, nameof(SystemMessage.ERR005));
            }
        }

        public async Task UpdateTimeTrackerAsync(Guid timeTrackerId, UpdateTimeTrackerDto timeTracker, CancellationToken cancellationToken)
        {
            try
            {
                timeTracker.Id = timeTrackerId;

                updateTimeTrackerValidator.ValidateAndThrow(timeTracker);

                var timeTrackerEntity = await repository.GetTimeTrackerByIdAsync(timeTrackerId, cancellationToken)
                    ?? throw new TimeTrackerException(string.Format(SystemMessage.ERR004, timeTrackerId), nameof(SystemMessage.ERR004));

                timeTrackerEntity.EndDate = ToDateTimeFromTimeSpan(timeTracker.EndTime, timeTracker.TimeZoneId);
                timeTrackerEntity.StartDate = ToDateTimeFromTimeSpan(timeTracker.StartTime, timeTracker.TimeZoneId) ?? DateTime.UtcNow;
                timeTrackerEntity.ProjectId = timeTracker.ProjectId;
                timeTrackerEntity.TaskId = timeTracker.TaskId;

                await repository.UpdateTimeTrackerAsync(timeTrackerEntity, cancellationToken);
            }
            catch (TimeTrackerException)
            {
                throw;
            }
            catch (ValidationException ex)
            {
                throw new DtoValidationException(ex);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, SystemMessage.ERR005);
                throw new TimeTrackerException(ex, SystemMessage.ERR005, nameof(SystemMessage.ERR005));
            }
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