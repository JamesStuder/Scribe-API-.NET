namespace Scribe.Api.Library.Models
{
    public class LookupTablesModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreationDate { get; set; }
        public string LastModificationDate { get; set; }
        public string ModificationBy { get; set; }
        public LookupTableValuesModel[] LookupTableValues { get; set; }
    }
}