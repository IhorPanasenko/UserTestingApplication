using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class UserTest
    {
        [Key]
        public int UserTestId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Test")]
        public int TestId { get; set; }

        public bool IsCompleted { get; set; }

        public int? Mark { get; set; }
    }
}
