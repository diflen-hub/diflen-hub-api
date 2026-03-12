namespace Domain.Dtos
{
    public class AlternativeDtoOut
    {
        public Guid PublicId { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}