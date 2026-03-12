namespace Domain.Dtos
{
    public class AnswerVerifyIn
    {
        public Guid PublicLessonId { get; set; }
        public required string UnityName { get; set; }
        public List<AlternativeDtoIn> Answers { get; set; } = [];
    }
}