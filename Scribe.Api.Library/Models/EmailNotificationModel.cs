namespace Scribe.Api.Library.Models
{
    public class EmailNotificationModel
    {
        public string OrgName { get; set; }
        public string TenantId { get; set; }
        public bool HeartbeatEmail { get; set; }
        public bool FailedRowsEmail { get; set; }
        public bool FailedJobEmail { get; set; }
        public bool ConnectorActivationEmail { get; set; }
        public bool EventFailedJobEmail { get; set; }
        public bool SystemUpdateEmail { get; set; }
    }
}