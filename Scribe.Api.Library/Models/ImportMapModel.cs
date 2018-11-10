namespace Scribe.Api.Library.Models
{
    public class ImportMapModel
    {
        public string Description { get; set; }
        public bool Enabled { get; set; }
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
        public string LastModificationDate { get; set; }
        public string LastRunDate { get; set; }
        public string LockedBy { get; set; }
        public string MapType { get; set; }
        public string MapTypeString { get; set; }
        public string Name { get; set; }
        public bool NetChange { get; set; }
        public string NetChangeFieldName { get; set; }
        public string ProcessDefinitionDbId { get; set; }
        public RelnameinformationlookupModel[] RelNameInformationLookup { get; set; }
        public string SourceConnectionAlias { get; set; }
        public string SourceConnectionId { get; set; }
        public string SourceConnectionName { get; set; }
        public string SourceConnectionType { get; set; }
        public string SourceEntity { get; set; }
        public string[] SourcePrimaryKeyPropertyFullNames { get; set; }
        public string TargetConnectionId { get; set; }
        public TargetConnectionInfoModel[] TargetConnectionInfo { get; set; }
        public string TargetConnectionName { get; set; }
        public string[] TargetConnectionNames { get; set; }
        public string TargetConnectionType { get; set; }
        public bool Valid { get; set; }
        public string Version { get; set; }
    }
}