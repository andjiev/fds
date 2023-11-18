using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FDS.Package.Service.Models
{
    public class PackageJson
    {
        [JsonPropertyName("dependencies")]
        public Dictionary<string, string> Dependencies { get; set; }
    }
}