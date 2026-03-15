namespace api.Controllers.Requests
{
    public class AnswerVerifyRequestDto
    {
        public Guid PublicLessonId { get; set; }
        public required string UnityName { get; set; }
        public List<AlternativeRequestDto> Answers { get; set; } = [];
    }
}