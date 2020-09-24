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
        public override int GetTotal()
        {
            return _dbContext.Set<Job>().Count();
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
                    State = (JobStateEnum)result.State,
                    ProcessedDate = result.Processed
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
            foreach (var result in results)
            {
                var job = new VYT.Models.Job
                {
                    Id = result.Id,
                    Name = result.Name,
                    CreatedDate = result.Created,
                    DocumentPages = result.DocumentPages,
                    Duration = result.Duration.HasValue ? TimeSpan.FromTicks(result.Duration.Value) : TimeSpan.Zero,
                    Languages = result.Languages,
                    Notes = result.Notes,
                    State = (JobStateEnum)result.State,
                    ProcessedDate = result.Processed
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
            _dbContext.usp_Job_Update(entity.Id, (int)entity.State, entity.Duration.Ticks, entity.Notes, entity.DocumentPages, entity.ProcessedDate);
        }

        public JobFile AddJobFile(int jobId, string filePath, ResultTypeEnum type)
        {
            var fi = new FileInfo(filePath);
            var result = _dbContext.usp_Job_AddFile(jobId, (int)type, fi.Length, Path.GetExtension(filePath), filePath).FirstOrDefault();
            if (result != null)
            {
                return new JobFile
                {
                    Id = result.Id,
                    FileExtension = result.FileType,
                    FileName = Path.GetFileName(filePath),
                    FilePath = filePath,
                    FileSize = fi.Length,
                    Type = type
                };
            }
            else
            {
                return null;
            }
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
                        Id = item.Id,
                        FileName = Path.GetFileName(item.FilePath),
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

        public IEnumerable<Models.Job> GetByState(JobStateEnum state, int limit)
        {
            var jobs = new List<Models.Job>();
            var results = _dbContext.usp_Job_GetByState((int)state, limit);
            foreach (var item in results)
            {
                jobs.Add(new Models.Job
                {
                    Id = item.Id,
                    CreatedDate = item.Created,
                    DocumentPages = item.DocumentPages,
                    Duration = item.Duration.HasValue ? TimeSpan.FromTicks(item.Duration.Value) : TimeSpan.Zero,
                    Languages = item.Languages,
                    Name = item.Name,
                    Notes = item.Notes,
                    State = (JobStateEnum)item.State
                });
            }
            return jobs;
        }
    }
}
