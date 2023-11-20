using Core.Models;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        public Task<AppUser?> GetById(string userId); 
    }
}
