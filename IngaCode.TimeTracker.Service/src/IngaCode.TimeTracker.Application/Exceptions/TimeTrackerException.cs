using IngaCode.TimeTracker.Domain.Contracts.Exceptions;

namespace IngaCode.TimeTracker.Application.Exceptions
{
    public class TimeTrackerException : Exception, IApplicationException
    {
        public TimeTrackerException
        (
            Exception innerException,
            string systemMessage,
            string codeMessage,
            params object[] parameters
        ) : base(systemMessage, innerException)
        {
            SystemMessage = systemMessage;
            CodeMessage = codeMessage;
            Args = parameters;
        }

        public TimeTrackerException
        (
            string systemMessage,
            string codeMessage,
            params object[] parameters
        ) : base(systemMessage)
        {
            SystemMessage = systemMessage;
            CodeMessage = codeMessage;
            Args = parameters;
        }

        public string SystemMessage { get; }
        public string CodeMessage { get; }
        public object[] Args { get; set; }
    }
}
