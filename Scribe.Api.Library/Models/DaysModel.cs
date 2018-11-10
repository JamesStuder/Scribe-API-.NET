namespace Scribe.Api.Library.Models
{
    public class DaysModel
    {
        public string DaysOption { get; set; }
        public string DaysIntervalStartDate { get; set; }
        public int DaysInterval { get; set; }
        public int[] DaysOfMonth { get; set; }
        public string[] DaysOfWeek { get; set; }
        public bool LastDayOfMonth { get; set; }
    }
}
