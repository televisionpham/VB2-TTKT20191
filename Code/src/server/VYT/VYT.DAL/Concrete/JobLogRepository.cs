using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYT.DAL.Abstract;

namespace VYT.DAL.Concrete
{
    internal class JobLogRepository : Repository<JobLog>, IJobLogRepository
    {
        public JobLogRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
