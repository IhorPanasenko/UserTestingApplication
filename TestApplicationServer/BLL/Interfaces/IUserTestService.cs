using Core.Models;

namespace BLL.Interfaces
{
    public interface IUserTestService
    {
        public Task<List<UserTest>?> GetUserTests(string userId);
    }
}
