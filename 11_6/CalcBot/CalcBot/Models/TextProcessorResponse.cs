namespace CalcBot.Models
{
    public struct TextProcessorResponse
    {
        public bool Ok { get; set; }
        public string Text { get; set; }
        public string ErrorMessage { get; set; }
    }
}
