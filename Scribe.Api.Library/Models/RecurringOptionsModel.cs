namespace Scribe.Api.Library.Models
{
    public class RecurringOptionsModel
    {
        public DaysModel Days { get; set; }
        public TimesModel Times { get; set; }
        public string TimeZone { get; set; }
    }
}
