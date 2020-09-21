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
        void AddJobFile(int jobId, string filePath, ResultTypeEnum type);
        IEnumerable<JobFile> GetJobFiles(int id, int type);
    }
}
