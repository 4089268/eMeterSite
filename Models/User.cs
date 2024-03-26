using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eMeterSite.Models
{
    public class User
    {
        [JsonPropertyName("id")]
        public long Id {get;set;}
        
        [JsonPropertyName("email")]
        [DataType(DataType.EmailAddress)]
        public string Email {get;set;} = "";
        
        [JsonPropertyName("name")]
        public string Name {get;set;} = "";
        
        [JsonPropertyName("company")]
        public string Company {get;set;} = "";
    }
}