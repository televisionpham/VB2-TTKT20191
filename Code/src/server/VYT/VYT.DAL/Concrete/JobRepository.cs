using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYT.DAL.Abstract;
using VYT.Models;

namespace VYT.DAL.Concrete
{
    internal class JobRepository : Repository<VYT.Models.Job>, IJobRepository
    {
        public JobRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override VYT.Models.Job Get(int id)
        {
            var result = _dbContext.usp_Job_Get(id).FirstOrDefault();
            if (result != null)
            {
                return new VYT.Models.Job()
                {
                    Id = result.Id,
                    Name = result.Name,
                    CreatedDate = result.Created,
                    DocumentPages = result.DocumentPages,
                    Duration = result.Duration.HasValue ? TimeSpan.FromTicks(result.Duration.Value) : TimeSpan.Zero,
                    Languages = result.Languages,
                    Notes = result.Notes,
                    State = (JobStateEnum)result.State
                };
            }
            else
            {
                return null;
            }
        }

        public override IEnumerable<VYT.Models.Job> GetPage(int pageIndex, int pageSize)
        {
            var jobs = new List<VYT.Models.Job>();
            var results = _dbContext.usp_Job_GetPage(pageIndex - 1, pageSize);
            foreach (var j in results)
            {
                var job = new VYT.Models.Job
                {
                    Id = j.Id,
                    Name = j.Name,
                    CreatedDate = j.Created,
                    DocumentPages = j.DocumentPages,
                    Duration = j.Duration.HasValue ? TimeSpan.FromTicks(j.Duration.Value) : TimeSpan.Zero,
                    Languages = j.Languages,
                    Notes = j.Notes,
                    State = (JobStateEnum)j.State
                };
                jobs.Add(job);
            }
            return jobs;
        }
        public override VYT.Models.Job Add(VYT.Models.Job entity)
        {
            var result = _dbContext.usp_Job_Add(entity.Name, entity.Languages).FirstOrDefault();
            if (result != null)
            {
                entity.Id = result.Id;
                entity.CreatedDate = result.Created;                       
            }

            return entity;
        }

        public override void Remove(int id)
        {
            _dbContext.usp_Job_Delete(id);
        }

        public override void Update(VYT.Models.Job entity)
        {
            _dbContext.usp_Job_Update(entity.Id, (int)entity.State, entity.Duration.Ticks, entity.Notes, entity.DocumentPages);
        }

        public void AddJobFile(int jobId, string filePath, ResultTypeEnum type)
        {
            var fi = new FileInfo(filePath);
            _dbContext.usp_Job_AddFile(jobId, (int)type, fi.Length, Path.GetExtension(filePath), filePath);
        }

        public IEnumerable<JobFile> GetJobFiles(int id, int type)
        {                        
            var files = new List<JobFile>();
            var job = Get(id);
            if (job != null)
            {

                var results = _dbContext.usp_Job_GetFile(id, type);
                foreach (var item in results)
                {
                    var jobFile = new JobFile()
                    {
                        FileName = job.Name,
                        FileExtension = item.FileType,
                        FilePath = item.FilePath,
                        FileSize = item.FileSize.HasValue ? item.FileSize.Value : 0,
                        Type = (ResultTypeEnum)item.Type
                    };

                    files.Add(jobFile);
                }
            }
            return files;
        }
    }
}
