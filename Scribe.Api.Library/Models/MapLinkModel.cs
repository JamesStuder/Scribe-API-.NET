namespace Scribe.Api.Library.Models
{
    public class MapLinkModel
    {
        public bool Enabled { get; set; }
        public bool Valid { get; set; }
        public int Id { get; set; }
        public int Index { get; set; }
        public string MapType { get; set; }
        public string Sources { get; set; }
        public string Targets { get; set; }
        public string Name { get; set; }
        public string LockedBy { get; set; }
        public string LockDate { get; set; }
        public string EventUrl { get; set; }
        public string LastModified { get; set; }
        public string ModifiedBy { get; set; }
        public string Description { get; set; }
    }
}