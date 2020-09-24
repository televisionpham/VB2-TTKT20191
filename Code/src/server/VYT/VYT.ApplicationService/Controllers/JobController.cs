using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using VYT.ApplicationService.Helpers;
using VYT.DAL;
using VYT.DAL.Abstract;
using VYT.Models;

namespace VYT.ApplicationService.Controllers
{
    [EnableCors(origins:"http://localhost:3001", headers: "*", methods: "*")]
    public class JobController : ApiController
    {
        private const string FILE_STORAGE = "~/FileStorage";
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IUnitOfWork _uow = DALFactory.GetInstance().CreateUnitOfWork();

        public VYT.Models.Job Get(int id)
        {
            return _uow.JobRepository.Get(id);
        }

        [HttpGet]
        [Route("api/Job/Total")]
        public int GetTotal()
        {
            return _uow.JobRepository.GetTotal();
        }

        public IEnumerable<VYT.Models.Job> GetPage(int pageIndex, int pageSize)
        {
            return _uow.JobRepository.GetPage(pageIndex, pageSize);
        }

        [HttpGet]
        [Route("api/Job/GetByState")]
        public IEnumerable<VYT.Models.Job> GetByState(JobStateEnum state, int limit)
        {
            return _uow.JobRepository.GetByState(state, limit);
        }

        [HttpPut]
        public void Update([FromBody]VYT.Models.Job job)
        {
            _uow.JobRepository.Update(job);            
        }

        [HttpDelete]
        public void Remove(int id)
        {
            _uow.JobRepository.Remove(id);
            var fileStorage = HttpContext.Current.Server.MapPath("~/FileStorage");
            var jobFolderFile = Path.Combine(fileStorage, id.ToString());
            FileUtil.DeleteFolder(jobFolderFile);
        }

        [ValidateMimeMultipartContentFilter]
        [HttpPost]
        [Route("api/Job/Create")]
        public async Task<Models.Job> CreateJobFromFile()
        {            
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var uploadFolder = HttpContext.Current.Server.MapPath(FILE_STORAGE);
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }            

            var provider = new MultipartFormDataStreamProvider(uploadFolder);

            try
            {                
                await Request.Content.ReadAsMultipartAsync(provider);
             
                foreach (MultipartFileData file in provider.FileData)
                {
                    //_logger.Trace($"{"Server file path: " + file.LocalFileName}; File Name: {file.Headers.ContentDisposition.FileName}");
                    try
                    {
                        var job = _uow.JobRepository.Add(new VYT.Models.Job()
                        {
                            Name = file.Headers.ContentDisposition.FileName.Trim('"'),
                            Languages = provider.FormData["languages"]
                        });

                        var fileStorage = Path.Combine(HttpContext.Current.Server.MapPath(FILE_STORAGE), job.Id.ToString());
                        if (!Directory.Exists(fileStorage))
                        {
                            Directory.CreateDirectory(fileStorage);
                        }

                        var filePath = Path.Combine(fileStorage, $"{job.Id}_0_{job.Name}");
                        filePath = FileUtil.NextAvailableFilename(filePath);
                        File.Move(file.LocalFileName, filePath);

                        _uow.JobRepository.AddJobFile(job.Id, filePath, ResultTypeEnum.InputImage);
                        return job;
                    }
                    catch (Exception)
                    {
                        FileUtil.DeleteFile(file.LocalFileName);
                        throw;
                    }
                }
                return null;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        [ValidateMimeMultipartContentFilter]
        [HttpPost]
        [Route("api/Job/AddFile")]
        public async Task<JobFile> AddJobFile()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var uploadFolder = HttpContext.Current.Server.MapPath(FILE_STORAGE);
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            var provider = new MultipartFormDataStreamProvider(uploadFolder);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                foreach (MultipartFileData file in provider.FileData)
                {                    
                    try
                    {
                        var jobId = provider.FormData["jobId"];
                        var job = _uow.JobRepository.Get(int.Parse(jobId));
                        if (job == null)
                        {
                            throw new Exception($"Không tìm thấy job ID: {jobId}");
                        }

                        var type = provider.FormData["jobFileType"];
                        var fileStorage = Path.Combine(HttpContext.Current.Server.MapPath(FILE_STORAGE), jobId);
                        if (!Directory.Exists(fileStorage))
                        {
                            Directory.CreateDirectory(fileStorage);
                        }
                        var fileName = file.Headers.ContentDisposition.FileName.Trim('"');
                        var filePath = Path.Combine(fileStorage, $"{jobId}_{type}{Path.GetExtension(fileName)}");
                        filePath = FileUtil.NextAvailableFilename(filePath);
                        File.Move(file.LocalFileName, filePath);

                        var jobFile = _uow.JobRepository.AddJobFile(job.Id, filePath, (ResultTypeEnum)(int.Parse(type)));
                        return jobFile;
                    }
                    catch (Exception ex)
                    {
                        FileUtil.DeleteFile(file.LocalFileName);
                        throw ex;
                    }                    
                }
                return null;
            }
            catch (System.Exception e)
            {
                _logger.Error(e);
                throw e;
            }
        }

        [HttpGet]
        [Route("api/Job/GetFiles")]
        public IEnumerable<JobFile> GetJobFiles(int id, int type)
        {
            var files = _uow.JobRepository.GetJobFiles(id, type);
            var baseUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath.TrimEnd('/');
            foreach (var file in files)
            {
                file.FilePath = $"{baseUrl}/FileStorage/{id}/{Path.GetFileName(file.FilePath)}";
            }
            return files;
        }
    }
}
