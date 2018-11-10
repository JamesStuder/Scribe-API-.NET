namespace Scribe.Api.Library.Settings
{
	public class UrlConnectorsSettings
	{
		public const string GET_Connectors_GetByOrg = "/v1/orgs/{orgId}/connectors";
		public const string POST_Connectors_Preconnect = "/v1/orgs/{orgId}/connectors/{connectorId}/preconnect";
		public const string GET_Connectors_GetPreconnectResults = "/v1/orgs/{orgId}/connectors/{connectorId}/preconnect/{commandId}";
		public const string GET_Connectors_GetConnectorVersion = "/v1/orgs/{orgId}/connectors/{connectorId}/version";
		public const string GET_Connectors_InstGetById = "/v1/orgs/{orgId}/connectors/{id}";
		public const string DELETE_Connectors_UninstallConnector = "/v1/orgs/{orgId}/connectors/{id}/delete";
		public const string POST_Connectors_InstallConnector = "/v1/orgs/{orgId}/connectors/{id}/install";
		public const string GET_Connectors_getConnectoruioptions = "/v1/orgs/{orgId}/connectors/{id}/uioptions";
	}
}