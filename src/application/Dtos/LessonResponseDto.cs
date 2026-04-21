namespace Application.Dtos
{
    public class LessonResponseDto
    {
        public Guid PublicId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? VideoUrl { get; set; }
        public bool Concluded { get; set; }
    }
}
