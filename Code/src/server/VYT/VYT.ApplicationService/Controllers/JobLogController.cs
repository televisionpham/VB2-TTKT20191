using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VYT.DAL;
using VYT.DAL.Abstract;

namespace VYT.ApplicationService.Controllers
{
    public class JobLogController : ApiController
    {
        private readonly IUnitOfWork _uow = DALFactory.GetInstance().CreateUnitOfWork();

        [HttpGet]
        public IEnumerable<VYT.Models.JobLog> GetPage(int pageIndex, int pageSize)
        {
            return _uow.JobLogRepository.GetPage(pageIndex, pageSize);
        }

        [HttpPost]
        public Models.JobLog Add([FromBody] Models.JobLog jobLog)
        {
            var ret = _uow.JobLogRepository.Add(jobLog);
            return ret;
        }
    }
}
