using System.Text.Json.Serialization;

namespace FDS.Package.Service.Models
{
    public class VersionJson
    {
        [JsonPropertyName("version")]
        public string Version { get; set; }
    }
}