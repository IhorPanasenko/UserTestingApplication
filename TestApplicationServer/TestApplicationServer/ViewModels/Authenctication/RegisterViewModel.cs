using System.ComponentModel.DataAnnotations;

namespace TestApplicationServer.ViewModels.Authenctication
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Login is required")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Passwoord is requird")]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        public string? Role { get; set; }
    }
}
