using Newtonsoft.Json;

namespace Scribe.Api.Library.Services
{
    public class JsonService
    {
        /// <summary>
        /// Method to serialized object to JSON string
        /// </summary>
        /// <param name="body">object with data for conversion</param>
        /// <returns>JSON String</returns>
        public string ConvertObjectToJSON(object body)
        {
            return JsonConvert.SerializeObject(body, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}