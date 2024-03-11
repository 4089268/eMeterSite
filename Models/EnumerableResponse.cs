using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eMeterSite.Models
{
    public class EnumerableResponse<T>
    {
        [JsonPropertyName("data")]
        public IEnumerable<T> Data { get; set; } = null!;

        [JsonPropertyName("chunkSize")]
        public int ChunkSize { get; set; }

        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("totalItems")]
        public int TotalItems { get; set; }
    }

}