using System.Text.Json.Serialization;

namespace StackExchangeService.Model
{
    public class Response<T>
    {
        [JsonPropertyName("items")]
        public IEnumerable<T> Items { get; set; } = new List<T>();
    }
}
