namespace Scribe.Api.Library.Models
{
    public class UserModel
    {
        public UnderPostDefaultsModel UnderPostDefaults { get; set; }
        public string[] OmittedFields { get; set; }
        public string Company { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string HttpReferrer { get; set; }
        public string Id { get; set; }
        public bool IsApproved { get; set; }
        public string JobTitle { get; set; }
        public string LastName { get; set; }
        public string LeadSource { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
    }
}