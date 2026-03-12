using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("unities")]
    public class Unity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;

        public required ICollection<Lesson> Lessons { get; set; }
    }
}