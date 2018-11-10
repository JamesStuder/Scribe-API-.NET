using Newtonsoft.Json;

namespace Scribe.Api.Library.Models
{
    public class AgentModel
    {
        /// <summary>
        /// Model used for Agents
        /// </summary>
        /// <param name="name">Name is required</param>
        public AgentModel(string name)
        {
            Name = name;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string MachineName { get; set; }
        public string Version { get; set; }
        public string ServiceName { get; set; }
        public string LastStartTime { get; set; }
        public string LastShutdownTime { get; set; }
        public string LastContactTime { get; set; }
        public bool IsCloudAgent { get; set; }
        public bool IsUpdating { get; set; }
        public string UsedInSolutions { get; set; }
        public string UpdateStateDateTime { get; set; }
        public InstalledConnectorModel[] InstalledConnectors { get; set; }
    }
}
