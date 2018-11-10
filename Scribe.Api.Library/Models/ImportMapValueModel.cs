namespace Scribe.Api.Library.Models
{
    public class ImportMapValueModel
    {
        public string ChildPropertyName { get; set; }
        public string EntityName { get; set; }
        public bool IsRequired { get; set; }
        public string ParentPropertyName { get; set; }
        public string RelationshipAlais { get; set; }
        public string RelationshipType { get; set; }
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Path { get; set; }
    }
}