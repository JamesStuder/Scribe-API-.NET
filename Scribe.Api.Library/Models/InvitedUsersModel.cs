namespace Scribe.Api.Library.Models
{
    public class InvitedUsersModel
    {
        public InvitedUsersModel() { }
        public InvitedUsersModel(string email, string status, string role)
        {
            Email = email;
            StatusType = status;
            RoleType = role;
        }
        public string Email { get; set; }
        public string InvitedDate { get; set; }
        public string StatusType { get; set; }
        public string RoleType { get; set; }
        public string Message { get; set; }

    }
}