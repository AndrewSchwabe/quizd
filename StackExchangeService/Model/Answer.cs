using System.Text.Json.Serialization;

namespace StackExchangeService.Model
{
    public class Answer
    {
        [JsonPropertyName("is_accepted")]
        public bool IsAccepted { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }
    }
}
