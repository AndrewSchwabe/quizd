namespace BlazorApp.Shared
{
    public class AnswerModel
    {
        public long Id { get; set; }
        public bool IsAccepted { get; set; }
        public string Body { get; set; }

        public string Class { get; set; } = string.Empty;
        public bool ButtonDisabled { get; set; } = false;
    }
}