namespace Scribe.Api.Library.Models
{
    public class OrgsModel
    {
        public OrgsModel() { }

        public OrgsModel(string name, int parentid)
        {
            Name = name;
            ParentId = parentid;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string[] Administrators { get; set; }
        public int ParentId { get; set; }
        public int TenantType { get; set; }
        public string Website { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string PrimaryContactFirstName { get; set; }
        public string PrimaryContactLastName { get; set; }
        public string PrimaryContactEmail { get; set; }
        public string PrimaryContactPhoneNumber { get; set; }
        public string PrimaryContactStreet { get; set; }
        public string PrimaryContactCity { get; set; }
        public string PrimaryContactState { get; set; }
        public string PrimaryContactPostalCode { get; set; }
        public string PrimaryContactCountry { get; set; }
        public bool IsSourceDataLocal { get; set; }
        public SecurityRuleModel[] SecurityRules { get; set; }
        public string ApiToken { get; set; }
        public bool IsAgentLogDownloadAllowed { get; set; }
        public string TenantRole { get; set; }
        public string Status { get; set; }
        public string[] SolutionStatusErrors { get; set; }
        public int DataCenterId { get; set; }
        public string DataCenterName { get; set; }
        public string DataCenterLocation { get; set; }
    }
}