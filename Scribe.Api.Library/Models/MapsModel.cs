namespace Scribe.Api.Library.Models
{
    public class MapsModel
    {
        public string StartingBlockId { get; set; }
        public TargetCollectionModel[] TargetConnections { get; set; }
        public string[] Blocks { get; set; }
        public int Revision { get; set; }
        public string RevisionDate { get; set; }
        public string RevisionComment { get; set; }
        public string Description { get; set; }
        public bool Valid { get; set; }
        public int Id { get; set; }
        public string LastModificationDate { get; set; }
        public string LastRunDate { get; set; }
        public string MapType { get; set; }
        public string Name { get; set; }
        public bool NetChange { get; set; }
        public string NetChangeFieldName { get; set; }
        public string ProcessDefinitionDbId { get; set; }
        public string SourceConnectionId { get; set; }
        public string SourceConnectionName { get; set; }
        public string SourceConnectionAlias { get; set; }
        public string SourceConnectionType { get; set; }
        public string Version { get; set; }
    }
}