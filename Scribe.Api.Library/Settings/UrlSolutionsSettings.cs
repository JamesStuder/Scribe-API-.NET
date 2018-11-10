namespace Scribe.Api.Library.Settings
{
	public class UrlSolutionsSettings
	{
		public const string GET_Solutions_GetSolutions = "/v1/orgs/{orgId}/solutions";
		public const string DELETE_Solutions_DeleteSolution = "/v1/orgs/{orgId}/solutions/{solutionId}";
		public const string POST_Solutions_CloneSolution = "/v1/orgs/{orgId}/solutions/{solutionId}/clone";
		public const string GET_Solutions_GetSolutionConnections = "/v1/orgs/{orgId}/solutions/{solutionId}/connections";
		public const string POST_Solutions_PrepareSolution = "/v1/orgs/{orgId}/solutions/{solutionId}/prepare";
		public const string GET_Solutions_PrepareSolutionStatus = "/v1/orgs/{orgId}/solutions/{solutionId}/prepare/{prepareId}";
		public const string GET_Solutions_GetSolutionSchedule = "/v1/orgs/{orgId}/solutions/{solutionId}/schedule";
		public const string POST_Solutions_Start = "/v1/orgs/{orgId}/solutions/{solutionId}/start";
		public const string POST_Solutions_StartMonitorSolution = "/v1/orgs/{orgId}/solutions/{solutionId}/startmonitor";
		public const string POST_Solutions_Stop = "/v1/orgs/{orgId}/solutions/{solutionId}/stop";
		public const string POST_Solutions_StopMonitorSolution = "/v1/orgs/{orgId}/solutions/{solutionId}/stopmonitor";
	}
}