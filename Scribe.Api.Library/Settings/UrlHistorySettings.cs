namespace Scribe.Api.Library.Settings
{
	public class UrlHistorySettings
	{
		public const string GET_History_GetHistorys = "/v1/orgs/{orgId}/solutions/{solutionId}/history";
		public const string GET_History_GetHistory = "/v1/orgs/{orgId}/solutions/{solutionId}/history/{id}";
		public const string POST_History_ExportHistory = "/v1/orgs/{orgId}/solutions/{solutionId}/history/{id}/export";
		public const string GET_History_RetrieveExportedHistory = "/v1/orgs/{orgId}/solutions/{solutionId}/history/{id}/export/{exportId}";
		public const string POST_History_MarkForReprocess = "/v1/orgs/{orgId}/solutions/{solutionId}/history/{id}/mark";
		public const string POST_History_ReprocessHistory = "/v1/orgs/{orgId}/solutions/{solutionId}/history/{id}/reprocess";
		public const string GET_History_GetHistoryStatistics = "/v1/orgs/{orgId}/solutions/{solutionId}/history/{id}/statistics";
	}
}