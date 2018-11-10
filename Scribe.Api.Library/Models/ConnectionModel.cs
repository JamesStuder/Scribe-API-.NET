namespace Scribe.Api.Library.Models
{
    public class ConnectionModel
    {
        public ConnectionModel() { }

        /// <summary>
        /// Method used to Connection
        /// </summary>
        /// <param name="name">Name is required</param>
        /// <param name="connectorId">Connector ID is required</param>
        /// <param name="color">Color in hexadecimal ARGB is required</param>
        public ConnectionModel(string name, string connectorId, string color)
        {
            Name = name;
            ConnectorId = connectorId;
            Color = color;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Color { get; set; }
        public string ConnectorId { get; set; }
        public string ConnectorType { get; set; }
        public string CreateDateTime { get; set; }
        public string LastModificationDateTime { get; set; }
        public string UsedInSolutions { get; set; }
        public PropertiesModel[] Properties { get; set; }
    }
}