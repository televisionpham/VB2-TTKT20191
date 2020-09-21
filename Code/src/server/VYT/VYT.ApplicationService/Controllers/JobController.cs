﻿using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using VYT.ApplicationService.Helpers;
using VYT.DAL;
using VYT.DAL.Abstract;
using VYT.Models;

namespace VYT.ApplicationService.Controllers
{
    public class JobController : ApiController
    {
        private const string FILE_STORAGE = "~/FileStorage";
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IUnitOfWork _uow = DALFactory.GetInstance().CreateUnitOfWork();

        public VYT.Models.Job Get(int id)
        {
            return _uow.JobRepository.Get(id);
        }

        public IEnumerable<VYT.Models.Job> GetPage(int pageIndex, int pageSize)
        {
            return _uow.JobRepository.GetPage(pageIndex, pageSize);
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
        public async Task<HttpResponseMessage> CreateJobFromFile()
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
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    _logger.Trace($"{"Server file path: " + file.LocalFileName}; File Name: {file.Headers.ContentDisposition.FileName}");                    

                    var job = _uow.JobRepository.Add(new VYT.Models.Job()
                    {
                        Name = file.Headers.ContentDisposition.FileName.Trim('"'),
                        Languages = provider.FormData["Languages"]
                    });

                    var fileStorage = Path.Combine(HttpContext.Current.Server.MapPath(FILE_STORAGE), job.Id.ToString());
                    if (!Directory.Exists(fileStorage))
                    {
                        Directory.CreateDirectory(fileStorage);
                    }

                    var filePath = Path.Combine(fileStorage, $"{job.Id}_{job.Name}");
                    filePath = FileUtil.NextAvailableFilename(filePath);
                    File.Move(file.LocalFileName, filePath);

                    _uow.JobRepository.AddJobFile(job.Id, filePath, ResultTypeEnum.InputImage);
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
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