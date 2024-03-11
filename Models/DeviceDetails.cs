#pragma warning disable CS8618 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eMeterSite.Models
{
    public class DeviceDetails
    {
        [JsonPropertyName("deviceAddress")]
        public string DeviceAddress { get; set; }

        [JsonPropertyName("device")]
        public DeviceInfo DeviceInfo { get; set; }

        [JsonPropertyName("measurement")]
        public List<Measurement> Measurement { get; set; }
    }
}