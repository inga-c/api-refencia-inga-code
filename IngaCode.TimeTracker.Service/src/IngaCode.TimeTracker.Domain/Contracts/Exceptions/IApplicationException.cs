namespace IngaCode.TimeTracker.Domain.Contracts.Exceptions
{
    public interface IApplicationException
    {
        string SystemMessage { get; }
        string CodeMessage { get; }
        object[] Args { get; set; }
    }
}
