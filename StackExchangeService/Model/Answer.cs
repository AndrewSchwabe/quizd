using System.Text.Json.Serialization;

namespace StackExchangeService.Model
{
    public class Answer
    {
        [JsonPropertyName("answer_id")]
        public long Id { get; set; }

        [JsonPropertyName("is_accepted")]
        public bool IsAccepted { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }
    }
}
