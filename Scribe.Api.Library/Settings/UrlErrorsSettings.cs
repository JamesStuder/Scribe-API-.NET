namespace Scribe.Api.Library.Settings
{
	public class UrlErrorsSettings
	{
		public const string GET_Errors_GetErrors = "/v1/orgs/{orgId}/solutions/{solutionId}/history/{historyId}/errors";
		public const string GET_Errors_GetError = "/v1/orgs/{orgId}/solutions/{solutionId}/history/{historyId}/errors/{id}";
		public const string POST_Errors_MarkForReprocess = "/v1/orgs/{orgId}/solutions/{solutionId}/history/{historyId}/errors/{id}/mark";
	}
}