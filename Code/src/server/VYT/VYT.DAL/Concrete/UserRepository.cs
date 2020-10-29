using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYT.DAL.Abstract;

namespace VYT.DAL.Concrete
{
    internal class UserRepository : Repository<Models.User>, IUserRepository
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override Models.User Add(Models.User entity)
        {
            var result = _dbContext.usp_User_Add(entity.Email, entity.PasswordHash).FirstOrDefault();
            if (result != null)
            {
                var user = new Models.User
                {
                    Id = result.Id,
                    Email = result.Email,
                    PasswordHash = result.PasswordHash
                };
                return user;
            }
            else
            {
                return null;
            }
        }

        public Models.User Get(string email, string passwordHash)
        {
            var result = _dbContext.usp_User_Get(email, passwordHash).FirstOrDefault();
            if (result != null)
            {
                var user = new Models.User
                {
                    Id = result.Id,
                    Email = result.Email,
                };
                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
