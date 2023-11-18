using System.Text.Json.Serialization;

namespace FDS.Common.Models
{
    public class VersionJson
    {
        [JsonPropertyName("version")]
        public string Version { get; set; }
    }
}