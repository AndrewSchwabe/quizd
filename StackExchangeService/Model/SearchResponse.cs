using System.Text.Json.Serialization;

namespace StackExchangeService.Model
{
    public class SearchResponse
    {
        [JsonPropertyName("items")]
        public IEnumerable<Question> Questions { get; set; } = new List<Question>();
    }
}
