using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("questions")]
    public class Question : BaseEntity
    {
        public string Statement { get; set; } = string.Empty;

        [Column("lesson_id")]
        public int LessonId { get; set; }

        [Column("unity_id")]
        public int UnityId { get; set; }

        public required ICollection<Alternative> Alternatives { get; set; }
        public required Unity Unity { get; set; }
    }
}