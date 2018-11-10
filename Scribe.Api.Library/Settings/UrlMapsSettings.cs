namespace Scribe.Api.Library.Settings
{
	public class UrlMapsSettings
	{
		public const string POST_Maps_UpgradeMap = "/v1/orgs/{orgId}/solution/{solutionId}/maps/advanced/upgrade";
		public const string GET_Maps_GetMapLinkById = "/v1/orgs/{orgId}/solutions/{solutionId}/maplinks/{mapId}";
		public const string GET_Maps_GetMaps = "/v1/orgs/{orgId}/solutions/{solutionId}/maps";
		public const string DELETE_Maps_DeleteMap = "/v1/orgs/{orgId}/solutions/{solutionId}/maps/{mapId}";
		public const string POST_Maps_ChangeBlockType = "/v1/orgs/{orgId}/solutions/{solutionId}/maps/{mapId}/changeblocktype";
		public const string POST_Maps_CloneMap = "/v1/orgs/{orgId}/solutions/{solutionId}/maps/{mapId}/clone";
		public const string PUT_Maps_EnableDisableMap = "/v1/orgs/{orgId}/solutions/{solutionId}/maps/{mapId}/enable";
		public const string GET_Maps_GetMapEventInfo = "/v1/orgs/{orgId}/solutions/{solutionId}/maps/{mapId}/eventinfo";
		public const string PUT_Maps_LockMap = "/v1/orgs/{orgId}/solutions/{solutionId}/maps/{mapId}/lock";
		public const string GET_Maps_GetNativeQueryResults = "/v1/orgs/{orgId}/solutions/{solutionId}/maps/{mapId}/nativequerytest";
		public const string POST_Maps_PreviewQuery = "/v1/orgs/{orgId}/solutions/{solutionId}/maps/{mapId}/previewquery";
		public const string GET_Maps_GetPreviewQueryResults = "/v1/orgs/{orgId}/solutions/{solutionId}/maps/{mapId}/previewquery/{previewId}";
		public const string GET_Maps_GetRelationships = "/v1/orgs/{orgId}/solutions/{solutionId}/maps/{mapId}/relationships";
		public const string POST_Maps_RenameBlock = "/v1/orgs/{orgId}/solutions/{solutionId}/maps/{mapId}/renameblock";
		public const string POST_Maps_RevertMap = "/v1/orgs/{orgId}/solutions/{solutionId}/maps/{mapId}/revert";
		public const string GET_Maps_GetMapRevisions = "/v1/orgs/{orgId}/solutions/{solutionId}/maps/{mapId}/revisions";
		public const string POST_Maps_RunMap = "/v1/orgs/{orgId}/solutions/{solutionId}/maps/{mapId}/run";
		public const string GET_Maps_GetRunMapResult = "/v1/orgs/{orgId}/solutions/{solutionId}/maps/{mapId}/run/{commandId}";
		public const string POST_Maps_ValidateMap = "/v1/orgs/{orgId}/solutions/{solutionId}/maps/{mapId}/validate";
		public const string POST_Maps_ValidateFormula = "/v1/orgs/{orgId}/solutions/{solutionId}/maps/{mapId}/validateformula";
		public const string GET_Maps_GetValidationMapResults = "/v1/orgs/{orgId}/solutions/{solutionId}/maps/{validationId}/validationresults";
		public const string POST_Maps_CreateAdvMap = "/v1/orgs/{orgId}/solutions/{solutionId}/maps/advanced";
		public const string PUT_Maps_UpdateAdvMap = "/v1/orgs/{orgId}/solutions/{solutionId}/maps/advanced/{mapId}";
		public const string POST_Maps_Export = "/v1/orgs/{orgId}/solutions/{solutionId}/maps/export";
		public const string GET_Maps_GetExportedMaps = "/v1/orgs/{orgId}/solutions/{solutionId}/maps/export/{exportId}";
		public const string POST_Maps_ImportMaps = "/v1/orgs/{orgId}/solutions/{solutionId}/maps/import";
	}
}