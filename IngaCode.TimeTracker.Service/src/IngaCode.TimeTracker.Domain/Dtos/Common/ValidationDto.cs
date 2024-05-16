namespace IngaCode.TimeTracker.Domain.Dtos.Common
{
    public class ValidationDto
    {
        public string PropertyName { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public object Value { get; set; }
        public Dictionary<string, object>? Args { get; set; }
    }
}
