using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VYT.Models;

namespace VYT.ProcessingStation.Service
{
    internal class JobManager : ManagerBase
    {
        public JobManager(OcrStation ocrStation) : base(ocrStation)
        {
        }

        public override void Start()
        {
            _isRunning = true;
            Task.Factory.StartNew(getJob);
        }

        public override void Stop()
        {
            _isRunning = false;
        }

        private void getJob()
        {
            try
            {
                while (_isRunning)
                {
                    try
                    {
                        var freeProcess = ControlCenter.GetInstance().ProcessManager.GetFreeProcess();
                        if (freeProcess == null)
                        {
                            continue;
                        }
                        
                        Thread.Sleep(TimeSpan.FromMilliseconds(500));
                        
                        if (!freeProcess.IsFree)
                        {
                            continue;
                        }

                        var result = _client.GetJobByState(JobStateEnum.Waiting, 1);
                        result.Wait();
                        if (result.Result.Count() == 0)
                        {
                            continue;
                        }
                        
                        var job = result.Result.FirstOrDefault();
                        _logger.Trace($"Đang xử lý job: {job.Name} (ID: {job.Id})");
                        try
                        {
                            job.State = JobStateEnum.Processing;
                            _client.UpdateJob(job).Wait();
                            var jobFolder = Path.Combine(_ocrStation.TempFolder, job.Id.ToString());
                            if (!Directory.Exists(jobFolder))
                            {
                                Directory.CreateDirectory(jobFolder);
                            }

                            var jobFiles = _client.GetJobFiles(job.Id, (int)ResultTypeEnum.InputImage);
                            jobFiles.Wait();
                            if (jobFiles.Result.Count() == 0)
                            {
                                job.State = JobStateEnum.Error;
                                job.Notes = $"Job không có file đầu vào: {job.Name} (ID: {job.Id})";
                                _logger.Warn(job.Notes);                                
                                _client.UpdateJob(job).Wait();
                                continue;
                            }

                            var inputFile = Path.Combine(jobFolder, job.Id + Path.GetExtension(job.Name));
                            FileUtil.DeleteFile(inputFile);
                            _client.DownloadFile(jobFiles.Result.FirstOrDefault().FilePath, inputFile).Wait();
                            freeProcess.ProcessJob(job, inputFile);
                        }
                        catch (Exception ex)
                        {
                            _logger.Error(ex);
                            job.State = JobStateEnum.Error;
                            job.Notes = ex.ToString();
                            _client.UpdateJob(job).Wait();
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex);
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(_ocrStation.AskingInterval));
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }
    }
}
