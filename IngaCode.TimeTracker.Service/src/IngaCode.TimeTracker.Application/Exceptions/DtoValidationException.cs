using FluentValidation;
using IngaCode.TimeTracker.Domain.Contracts.Exceptions;
using IngaCode.TimeTracker.Domain.Dtos.Common;

namespace IngaCode.TimeTracker.Application.Exceptions
{
    public class DtoValidationException : Exception, IDtoValidationException
    {
        public DtoValidationException(ValidationException validationException)
        {
            ValidationErrors = validationException.Errors.Select(x =>
            {
                var validationModel = new ValidationDto
                {
                    PropertyName = x.PropertyName,
                    Code = x.ErrorCode,
                    Message = x.ErrorMessage,
                    Value = x.CustomState ?? x.AttemptedValue,
                };

                if (x.FormattedMessagePlaceholderValues.Any())
                {
                    validationModel.Args = x.FormattedMessagePlaceholderValues.ToDictionary(y => y.Key, y => y.Value);
                }

                return validationModel;
            });
        }

        public IEnumerable<ValidationDto> ValidationErrors { get; private set; }
    }
}
