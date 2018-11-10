namespace Scribe.Api.Library.Models
{
    public class RegisterModel
    {

        public class Rootobject
        {
            public string[] Connectors { get; set; }
            public int OrgId { get; set; }
            public string OrgName { get; set; }
            public SubscriptionSkuModel[] SubscriptionSkus { get; set; }
            public SecurityRuleModel[] SecurityRules { get; set; }
            public string UserId { get; set; }
            public int DataCenterId { get; set; }
        }
    }
}