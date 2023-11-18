using Microsoft.AspNetCore.Identity;


namespace Core.Models
{
    public class AppUser : IdentityUser
    {
        public List<UserTest> UserTests { get; set; } = new List<UserTest>();
    }
}
