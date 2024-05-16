using IngaCode.TimeTracker.Domain.Dtos.Common;

namespace IngaCode.TimeTracker.Domain.Contracts.Exceptions
{
    public interface IDtoValidationException
    {
        IEnumerable<ValidationDto> ValidationErrors { get; }
    }
}
