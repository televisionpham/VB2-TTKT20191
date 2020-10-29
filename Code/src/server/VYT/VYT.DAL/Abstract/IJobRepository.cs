using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYT.Models;

namespace VYT.DAL.Abstract
{
    public interface IJobRepository : IRepository<VYT.Models.Job>
    {
        Models.Job Add(int userId, Models.Job job);
        JobFile AddJobFile(int jobId, string filePath, ResultTypeEnum type);
        IEnumerable<JobFile> GetJobFiles(int id, int type);

        IEnumerable<Models.Job> GetByState(int userId, JobStateEnum state, int limit);
        IEnumerable<Models.Job> GetPage(int uesrId, int pageIndex, int pageSize);
    }
}
