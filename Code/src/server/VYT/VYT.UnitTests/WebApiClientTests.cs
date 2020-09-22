﻿using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VYT.ApplicationServiceClient;

namespace VYT.UnitTests
{
    [TestClass]
    public class WebApiClientTests
    {
        private WebApiClient _client = new WebApiClient("http://localhost/VYT.ApplicationService");
        [TestMethod]
        public void Can_get_job()
        {            
            var resp = _client.GetJob(9);
            resp.Wait();
            Assert.IsNotNull(resp.Result);
            Assert.AreEqual(9, resp.Result.Id);
        }

        [TestMethod]
        public void Can_get_job_log_page()
        {
            var resp = _client.GetJobLogPage(1, 10);
            resp.Wait();
            Assert.IsTrue(resp.Result.Count() > 0);
        }

        [TestMethod]
        public void Can_add_jog_log()
        {
            var jobLog = new Models.JobLog
            {
                CreatedDate = DateTime.Now,
                DocumentPages = 0,
                Duration = TimeSpan.Zero,
                Name = "Test",
                State = Models.JobStateEnum.Processed,
                ProcessedDate = DateTime.Now,
                Notes = "Some notes"
            };
            var resp = _client.AddJobLog(jobLog);
            resp.Wait();
            Assert.IsTrue(resp.Result.Id > 0);
            Assert.AreEqual("Test", resp.Result.Name);
        }

        [TestMethod]
        public void Can_get_job_files()
        {
            var resp = _client.GetJobFiles(9, -1);
            resp.Wait();
            Assert.IsTrue(resp.Result.Count() > 0);
            Assert.AreEqual("CT20UB.tif", resp.Result.First().FileName);
        }

        [TestMethod]
        public void Can_update_job()
        {
            var pages = 1000;
            var resp = _client.GetJob(9);
            resp.Wait();
            var job = resp.Result;
            job.DocumentPages = pages;
            _client.UpdateJob(job).Wait();            
            resp = _client.GetJob(9);
            resp.Wait();
            Assert.AreEqual(pages, resp.Result.DocumentPages);
        }

        [TestMethod]
        public void Can_create_job_from_file()
        {
            var filePath = @"D:\DKC\AnhMau\Original\Cong van nghieng.TIF";
            var resp = _client.CreateJob(filePath, "vie");
            resp.Wait();
            Assert.AreEqual(Path.GetFileName(filePath), resp.Result.Name);
        }

        [TestMethod]
        public void Can_get_total_jobs()
        {
            var resp = _client.GetTotalJobs();
            resp.Wait();
            Assert.IsTrue(resp.Result > 0);
        }

        [TestMethod]
        public void Can_get_total_job_logs()
        {
            var resp = _client.GetTotalJobLogs();
            resp.Wait();
            Assert.IsTrue(resp.Result > 0);
        }
    }
}
