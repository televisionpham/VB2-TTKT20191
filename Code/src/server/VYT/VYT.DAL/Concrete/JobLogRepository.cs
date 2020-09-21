using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYT.DAL.Abstract;
using VYT.Models;

namespace VYT.DAL.Concrete
{
    internal class JobLogRepository : Repository<Models.JobLog>, IJobLogRepository
    {
        public JobLogRepository(DbContext dbContext) : base(dbContext)
        {            
        }

        public override Models.JobLog Add(Models.JobLog entity)
        {
            var result = _dbContext.usp_JobLog_Add(entity.Name, entity.CreatedDate, (int)entity.State, entity.DocumentPages, entity.Duration.Ticks, entity.ProcessedDate, entity.Notes)
                .FirstOrDefault();
            if (result != null)
            {
                entity.Id = result.Id;
                return entity;
            }
            else
            {
                return null;
            }
        }

        public override IEnumerable<Models.JobLog> GetPage(int pageIndex, int pageSize)
        {
            var jobLogs = new List<Models.JobLog>();
            var results = _dbContext.usp_JobLog_GetPage(pageIndex - 1, pageSize);
            foreach (var item in results)
            {
                jobLogs.Add(new Models.JobLog
                {
                    CreatedDate = item.Created,
                    DocumentPages = item.DocumentPages,
                    Duration = item.Duration.HasValue ? TimeSpan.FromTicks(item.Duration.Value) : TimeSpan.Zero,
                    Id = item.Id,
                    Name = item.Name,
                    Notes = item.Notes,
                    ProcessedDate = item.Processed,
                    State = (JobStateEnum)item.State,
                });
            }
            return jobLogs;
        }
    }
}
