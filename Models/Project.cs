using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eMeterSite.Models
{
    public class Project
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("proyecto")]
        public string Proyecto { get; set; } = "";

        [JsonPropertyName("clave")]
        public string Clave { get; set; } = "";
    }
}