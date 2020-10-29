using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VYT.DAL.Abstract
{
    public interface IUserRepository : IRepository<Models.User>
    {
        Models.User Get(string email, string passwordHash);
    }
}
