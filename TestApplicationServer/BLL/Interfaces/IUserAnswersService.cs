using Core.Models;

namespace BLL.Interfaces
{
    public interface IUserAnswersService
    {
        public Task<List<UserAnswer>?> GetByUSerTest(int usertTestId);
    }
}
