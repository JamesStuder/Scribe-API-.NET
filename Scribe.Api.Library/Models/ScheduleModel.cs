namespace Scribe.Api.Library.Models
{
    public class ScheduleModel
    {
        public string ScheduleOption { get; set; }
        public string SolutionId { get; set; }
        public RunOnceOptionsModel RunOnceOptions { get; set; }
        public RecurringOptionsModel RecurringOptions { get; set; }
    }
}