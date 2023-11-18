using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Test
    {
        [Key]
        public int TestId { get; set; }

        [Required]
        public string TestName { get; set; } = string.Empty;
        public List<Question> Questions { get; set; } = new List<Question>();
        public List<UserTest> UserTests { get; set; } = new List<UserTest>();
    }
}
