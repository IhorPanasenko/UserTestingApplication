

using Core.Models;

namespace BLL.Interfaces
{
    public interface IAuthenticationServer
    {
        public Task<bool> RegisterAsync(RegisterUserModel registerUser);

        public Task<string?> LoginAsync(LoginUserModel loginUser);
    }
}
