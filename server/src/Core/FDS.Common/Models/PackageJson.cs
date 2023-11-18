using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FDS.Common.Models
{
    public class PackageJson
    {
        [JsonPropertyName("dependencies")]
        public Dictionary<string, string> Dependencies { get; set; }
    }
}