

using BLL.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;

        public AuthenticationService(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
        }
        public async Task<string?> LoginAsync(LoginUserModel loginUser)
        {
            var user = await userManager.FindByNameAsync(loginUser.Login);

            if (user is null)
            {
                throw new ArgumentException("User with such Login doesn't exists");
            }

            if (!await userManager.CheckPasswordAsync(user, loginUser.Password))
            {
                throw new Exception("Wrong password");
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("UserId", user.Id)
            };

            var userRoles = await userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
                authClaims.Add(new Claim("UserRole", role));
            }

            var jwtToken = generateJwtToken(authClaims);
            return jwtToken;
        }

        public async Task<bool> RegisterAsync(RegisterUserModel registerUser)
        {
            var userExists = await userManager.FindByEmailAsync(registerUser.Email);

            if (userExists is not null)
            {
                throw new ArgumentException("User with such email Already exists");
            }

            if (registerUser.Password != registerUser.ConfirmPassword)
            {
                throw new ArgumentException("Password and confirm password should be equal");
            }

            var role = registerUser.Role ?? "User";

            if (await roleManager.FindByNameAsync(role) is null)
            {
                throw new ArgumentException("Provided Role doesn't exist");
            }

            AppUser newUser = new()
            {
                Email = registerUser.Email,
                UserName = registerUser.Login,
            };

            var result = await userManager.CreateAsync(newUser, registerUser.Password);

            if (!result.Succeeded)
            {
                StringBuilder errorMessage = new StringBuilder();
                errorMessage.AppendLine("Failed to create a User");

                foreach (IdentityError identityError in result.Errors)
                {
                    errorMessage.AppendLine(identityError.Description);
                };

                throw new Exception(errorMessage.ToString());
            }

            await userManager.AddToRoleAsync(newUser, role);
            return true;
        }

        private string generateJwtToken(List<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration!["Jwt:Secret"]!));

            var jwtToken = new JwtSecurityToken(
                issuer: configuration["Jwt:ValidIssuer"],
                audience: configuration["Jwt:ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;
        }
    }
}
