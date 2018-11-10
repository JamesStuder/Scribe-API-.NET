namespace Scribe.Api.Library.Settings
{
	public class UrlOrganizationsSettings
	{
		public const string GET_Organizations_GetOrganizations = "/v1/orgs";
		public const string DELETE_Organizations_DeleteOrganization = "/v1/orgs/{orgId}";
		public const string GET_Organizations_GetEndpoints = "/v1/orgs/{orgId}/endpoints";
		public const string PUT_Organizations_clearendpoint = "/v1/orgs/{orgId}/endpoints/{endpointid}/clear";
		public const string GET_Organizations_GetFunctions = "/v1/orgs/{orgId}/Functions";
		public const string GET_Organizations_GetSecurityRules = "/v1/orgs/{orgId}/securityrules";
		public const string DELETE_Organizations_DeleteSecurityRules = "/v1/orgs/{orgId}/securityrules/{ruleId}";
		public const string GET_Organizations_GetSecuritySettings = "/v1/orgs/{orgId}/SecuritySettings";
		public const string PUT_Organizations_resetaccesstoken = "/v1/orgs/{orgId}/SecuritySettings/resetaccesstoken";
		public const string PUT_Organizations_resetcryptotoken = "/v1/orgs/{orgId}/SecuritySettings/resetcryptotoken";
		public const string GET_Organizations_GetSettings = "/v1/orgs/{orgId}/Settings";
		public const string DELETE_Organizations_DeleteUser = "/v1/orgs/{orgId}/users";
	}
}