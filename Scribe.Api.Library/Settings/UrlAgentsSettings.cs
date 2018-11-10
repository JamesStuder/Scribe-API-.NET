namespace Scribe.Api.Library.Settings
{
	public class UrlAgentsSettings
	{
		public const string GET_Agents_GetAgents = "/v1/orgs/{orgId}/agents";
		public const string POST_Agents_CaptureAgentLogs = "/v1/orgs/{orgId}/agents/{agentId}/logs";
		public const string GET_Agents_GetAgentLogs = "/v1/orgs/{orgId}/agents/{agentId}/logs/{logId}";
		public const string POST_Agents_RestartAgent = "/v1/orgs/{orgId}/agents/{agentId}/restart";
		public const string DELETE_Agents_DeleteAgent = "/v1/orgs/{orgId}/agents/{id}";
        public const string GET_Agents_GetAgent = "/v1/orgs/{orgId}/agents/{id}";
        public const string PUT_Agents_ModifyAgent = "/v1/orgs/{orgId}/agents/{id}";
        public const string POST_Agents_ProvisionCloudAgent = "/v1/orgs/{orgId}/agents/provision_cloud_agent";
		public const string POST_Agents_ProvisionOnPremiseAgent = "/v1/orgs/{orgId}/agents/provision_onpremise_agent";
	}
}