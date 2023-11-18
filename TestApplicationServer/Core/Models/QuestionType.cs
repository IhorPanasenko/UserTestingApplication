using Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class QuestionType
    {
        [Key]
        public int QuestionTypeId { get; set; }

        [Required]
        [EnumDataType(typeof(EnumQuestionType))]
        public EnumQuestionType questionTypeName { get; set; }

        public List<Question> Questions { get; set; } = new List<Question>();

    }
}
