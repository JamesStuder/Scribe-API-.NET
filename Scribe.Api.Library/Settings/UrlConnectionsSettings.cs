namespace Scribe.Api.Library.Settings
{
	public class UrlConnectionsSettings
	{
		public const string GET_Connections_GetConnections = "/v1/orgs/{orgId}/connections";
        public const string POST_Connections_CreateConnection = "/v1/orgs/{orgId}/connections";
        public const string DELETE_Connections_DeleteConnection = "/v1/orgs/{orgId}/connections/{connectionId}";
        public const string GET_Connections_GetConnection = "/v1/orgs/{orgId}/connections/{connectionId}";
        public const string PUT_Connections_ModifyConnection = "/v1/orgs/{orgId}/connections/{connectionId}";
        public const string GET_Connections_GetConnectionActions = "/v1/orgs/{orgId}/connections/{connectionId}/actions";
		public const string GET_Connections_GetEntitiesForConnection = "/v1/orgs/{orgId}/connections/{connectionId}/entities";
		public const string GET_Connections_GetEntity = "/v1/orgs/{orgId}/connections/{connectionId}/entities/{entityIdOrName}";
		public const string GET_Connections_GetEntityFields = "/v1/orgs/{orgId}/connections/{connectionId}/entities/{entityIdOrName}/fields";
		public const string GET_Connections_GetEntityRelationships = "/v1/orgs/{orgId}/connections/{connectionId}/entities/{entityIdOrName}/relationships";
		public const string GET_Connections_GetEntityNamesForConnection = "/v1/orgs/{orgId}/connections/{connectionId}/entitynames";
		public const string POST_Connections_ResetMetadata = "/v1/orgs/{orgId}/connections/{connectionId}/reset";
		public const string GET_Connections_GetConnectionSupportedOperationsByCategory = "/v1/orgs/{orgId}/connections/{connectionId}/supportedactions";
		public const string POST_Connection_Test = "/v1/orgs/{orgId}/connections/{connectionId}/test";
		public const string POST_Connections_Test = "/v1/orgs/{orgId}/connections/test";
		public const string GET_Connections_Test = "/v1/orgs/{orgId}/connections/test/{commandId}";
	}
}