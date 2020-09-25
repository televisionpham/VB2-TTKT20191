using System;
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
        public void Can_get_job_by_state()
        {
            var resp = _client.GetJobByState(0, 1);
            resp.Wait();
            Assert.AreEqual(1, resp.Result.Count());
        }

        [TestMethod]
        public void Can_download_file()
        {
            var outputFile = "E:\\Temp\\out.tif";
            var resp = _client.DownloadFile("http://localhost/VYT.ApplicationService/FileStorage/9/9_CT20UB.tif", outputFile);
            resp.Wait();
            Assert.IsTrue(File.Exists(outputFile));
        }

        [TestMethod]
        public void Can_add_job_file()
        {
            var filePath = @"D:\DKC\AnhMau\Original\Cong van nghieng.TIF";
            var resp = _client.AddJobFile(9, filePath, Models.ResultTypeEnum.OcrResult);
            resp.Wait();
            Assert.IsTrue(resp.Result.Id > 0);
        }
    }
}
