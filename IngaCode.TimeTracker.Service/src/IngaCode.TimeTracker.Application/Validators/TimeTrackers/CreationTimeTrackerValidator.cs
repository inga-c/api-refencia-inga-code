using FluentValidation;
using IngaCode.TimeTracker.Domain.Dtos.TimeTrackers;

namespace IngaCode.TimeTracker.Application.Validators.TimeTrackers
{
    internal class CreationTimeTrackerValidator : AbstractValidator<CreationTimeTrackerDto>
    {
        public CreationTimeTrackerValidator()
        {
            RuleFor(x => x.StartTime)
                .NotEmpty();
                //.WithErrorCode(nameof(SystemMessage.MVD001))
                //.WithMessage(SystemMessage.MVD001)
                //.WithName(Label.StartTime);

            RuleFor(x => x.TimeZoneId)
                .NotEmpty();
                //.WithErrorCode(nameof(SystemMessage.MVD001))
                //.WithMessage(SystemMessage.MVD001)
                //.WithName(Label.TimeZoneId);
        }
    }
}
