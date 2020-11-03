using NLog;
using System.Web.Http;
using System.Web.Http.Cors;
using VYT.DAL;
using VYT.DAL.Abstract;

namespace VYT.ApplicationService.Controllers
{    
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IUnitOfWork _uow = DALFactory.GetInstance().CreateUnitOfWork();

        [HttpPost]
        [Route("api/User/Auth")]
        public Models.User Get(string email, string passwordHash)
        {
            try
            {
                var user = _uow.UserRepository.Get(email, passwordHash);
                if (user == null)
                {
                    throw new System.Exception("Email hoặc Password không chính xác");
                }
                return user;
            }
            catch (System.Exception ex)
            {
                _logger.Error(ex);
                throw ex;
            }
        }

        [HttpPost]
        [Route("api/User/Add")]
        public Models.User Add([FromBody] Models.User user)
        {
            try
            {
                var result = _uow.UserRepository.Add(user);
                return result;
            }
            catch (System.Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }
    }
}
