using System.Text.Json.Serialization;

namespace StackExchangeService.Model
{
    public class Question
    {

        [JsonPropertyName("question_id")]
        public long Id { get; set; }

        [JsonPropertyName("is_answered")]
        public bool IsAnswered { get; set; }

        [JsonPropertyName("answer_count")]
        public int AnswerCount { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

    }
}
