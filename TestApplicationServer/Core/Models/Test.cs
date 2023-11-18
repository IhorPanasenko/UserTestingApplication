using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Test
    {
        [Key]
        public int TestId { get; set; }

        [Required]
        public string TestName { get; set; } = string.Empty;
    }
}
