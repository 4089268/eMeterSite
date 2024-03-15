using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eMeterSite.Models
{
    public class AuthenticationResponse
    {
        [JsonPropertyName("title")]
        
        public string? Title { get; set; }

        [JsonPropertyName("token")]
        public string? Token { get; set; }
    }
}