

namespace Core.Models
{
    public class RegisterUserModel
    {
        public string? Login { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? ConfirmPassword { get; set; }

        public string? Role { get; set; }
    }
}
