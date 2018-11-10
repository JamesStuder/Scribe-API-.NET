namespace Scribe.Api.Library.Models
{
    public class SolutionsModel
    {
        public SolutionsModel() { }
        public SolutionsModel(string name, string agentId, string solutionType)
        {
            Name = name;
            AgentId = agentId;
            SolutionType = solutionType;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string AgentId { get; set; }
        public string Description { get; set; }
        public string ConnectionIdForSource { get; set; }
        public string ConnectionIdForTarget { get; set; }
        public string SolutionType { get; set; }
        public string Status { get; set; }
        public string InProgressStartTime { get; set; }
        public string LastRunTime { get; set; }
        public string NextRunTime { get; set; }
        public MapLinkModel[] MapLinks { get; set; }
        public bool IsDisabled { get; set; }
        public int ReasonDisabled { get; set; }
        public string MinAgentVersion { get; set; }
        public string ModificationBy { get; set; }
        public ReplicationSettingsModel ReplicationSettings { get; set; }
    }
}