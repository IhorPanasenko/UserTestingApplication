using Core.Models;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<UserRepository> logger;

        public UserRepository(ILogger<UserRepository> logger, ApplicationDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public async Task<AppUser?> GetById(string userId)
        {
            try
            {
                var user = await dbContext.AppUsers.FindAsync(userId);

                if (user is null)
                {
                    logger.LogInformation($"No user with Id {userId}");
                }

                return user;
            }
            catch(Exception ex)
            {
                logger.LogError($"Error while trying to get User by Id\n {ex.Message}");
                return null;
            }
        }
    }
}
