using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }

        [ForeignKey("Test")]
        public int TestId { get; set; }

        [Required]
        public string QuestionText { get; set; } = string.Empty;

        [ForeignKey("QuestionType")]
        public int QuestionTypeId { get; set; }

        public int Points { get; set; }

    }
}
