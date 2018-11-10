namespace Scribe.Api.Library.Settings
{
	public class UrlLookupTablesSettings
	{
		public const string GET_LookupTables_GetLookupTables = "/v1/orgs/{orgId}/lookuptables";
		public const string DELETE_LookupTables_DeleteLookupTable = "/v1/orgs/{orgId}/lookuptables/{tableId}";
		public const string GET_LookupTables_ExportLookupTable = "/v1/orgs/{orgId}/lookuptables/{tableId}/export";
		public const string GET_LookupTables_RetrieveExportedLookupTable = "/v1/orgs/{orgId}/lookuptables/{tableId}/export/{exportId}";
		public const string POST_LookupTables_ImportLookupTableContentCsv = "/v1/orgs/{orgId}/lookuptables/{tableId}/import";
		public const string GET_LookupTables_GetLookupTableValues = "/v1/orgs/{orgId}/lookuptables/{tableId}/values";
		public const string DELETE_LookupTables_DeleteLookupTableValue = "/v1/orgs/{orgId}/lookuptables/{tableId}/values/{valueId}";
		public const string GET_LookupTables_GetLookupTableValueValue1 = "/v1/orgs/{orgId}/lookuptables/{tableIdOrName}/values/value1/{value2}";
		public const string GET_LookupTables_GetLookupTableValueValue2 = "/v1/orgs/{orgId}/lookuptables/{tableIdOrName}/values/value2/{value1}";
	}
}