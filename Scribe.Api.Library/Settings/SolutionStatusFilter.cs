namespace Scribe.Api.Library.Settings
{
    public class SolutionStatusFilter
    {
        public const string Disabled = "Disabled";
        public const string Idle = "Idle";
        public const string IdleLastRunRowErrors = "IdleLastRunRowErrors";
        public const string IdleLastRunFailed = "IdleLastRunFailed";
        public const string InProgress = "InProgress";
        public const string Preparing = "Preparing";
        public const string Provisioning = "Provisioning";
        public const string ProvisionError = "ProvisionError";
        public const string Starting = "Starting";
        public const string Incomplete = "Incomplete";
        public const string Stopping = "Stopping";
        public const string OnDemand = "OnDemand";
        public const string OnDemandLastRunFailed = "OnDemandLastRunFailed";
        public const string OnDemandLastRunRowErrors = "OnDemandLastRunRowErrors";
        public const string AgentUpdating = "AgentUpdating";
        public const string AgentShutdown = "AgentShutdown";
        public const string AgentHeartbeatLate = "AgentHeartbeatLate";
        public const string AgentHeartbeatFailed = "AgentHeartbeatFailed";
        public const string WaitingForEvent = "WaitingForEvent";
        public const string WaitingToUpdateAgent = "WaitingToUpdateAgent";
        public const string WaitingToUpdateConnector = "WaitingToUpdateConnector";
        public const string UpdatingConnector = "UpdatingConnector";
        public const string WaitingToRestartAgentUserRequest = "WaitingToRestartAgentUserRequest";
        public const string WaitingToRestartAgentMemoryLimit = "WaitingToRestartAgentMemoryLimit";
        public const string AgentRestarting = "AgentRestarting";
    }
}