namespace Domain.Dtos
{
    public class QuestionDtoOut
    {
        public Guid PublicId { get; set; }
        public string Statement { get; set; } = string.Empty;

        public IEnumerable<AlternativeDtoOut> Alternatives { get; set; } = [];
    }
}