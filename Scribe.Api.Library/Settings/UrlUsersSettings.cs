namespace Scribe.Api.Library.Settings
{
	public class UrlUsersSettings
	{
		public const string GET_Users_GetUser = "/v1/users";
		public const string GET_Users_GetAlertSettings = "/v1/users/{userId}/alertsettings";
		public const string GET_Users_invitations = "/v1/users/invitations";
		public const string PUT_Users_AcceptInvitations = "/v1/users/invitations/{inviteId}";
	}
}