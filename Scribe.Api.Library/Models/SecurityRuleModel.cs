namespace Scribe.Api.Library.Models
{
    public class SecurityRuleModel
    {
        public string Name { get; set; }
        public bool ApiAccessEnabled { get; set; }
        public bool EventSolutionAccessEnabled { get; set; }
        public string AllowedIpRangeStartAddress { get; set; }
        public string AllowedIpRangeEndAddress { get; set; }
        public string id { get; set; }
    }
}