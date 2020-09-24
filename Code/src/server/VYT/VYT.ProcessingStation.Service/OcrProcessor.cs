using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYT.ApplicationServiceClient;
using VYT.Models;

namespace VYT.ProcessingStation.Service
{
    public class OcrProcessor
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly OcrStation _ocrStation;
        private WebApiClient _client;
        private static Process _process;

        public OcrProcessor(OcrStation ocrStation)
        {
            this._ocrStation = ocrStation;
            var baseAddress = ConfigurationManager.AppSettings["web-api-address"];
            _client = new WebApiClient(baseAddress);
        }

        public bool IsFree { get; set; } = true;

        public void ProcessJob(Models.Job job, string input)
        {
            try
            {
                var outputFolder = Path.Combine(_ocrStation.TempFolder, job.Id.ToString());
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }

                var output = Path.Combine(outputFolder, ((int)ResultTypeEnum.OcrResult).ToString());
                var execFile = ConfigurationManager.AppSettings["tesseract-path"];
                _process = new Process();
                _process.EnableRaisingEvents = true;
                _process.Exited += Process_Exited;
                _process.StartInfo.FileName = execFile;
                _process.StartInfo.Arguments = $"{input} {output} -l {job.Languages} pdf";
                _process.StartInfo.RedirectStandardOutput = true;
                _process.StartInfo.RedirectStandardError = true;
                _process.StartInfo.UseShellExecute = false;
                _process.StartInfo.CreateNoWindow = true;
                _process.OutputDataReceived += Process_OutputDataReceived;
                _process.ErrorDataReceived += Process_ErrorDataReceived;
                try
                {
                    IsFree = false;
                    var startTime = DateTime.Now;
                    _process.Start();
                    _process.BeginOutputReadLine();
                    _process.BeginErrorReadLine();
                    _process.WaitForExit();
                    var outputFile = output + ".pdf";
                    if (File.Exists(outputFile))
                    {
                        var now = DateTime.Now;
                        job.Duration = now - startTime;
                        job.DocumentPages = PdfUtil.GetTotalPages(outputFile);
                        var jobFile = _client.AddJobFile(job.Id, outputFile, ResultTypeEnum.OcrResult);
                        jobFile.Wait();
                        job.State = JobStateEnum.Processed;
                        job.ProcessedDate = now;
                        _client.UpdateJob(job).Wait();                    
                    }
                    else
                    {
                        job.State = JobStateEnum.Error;
                        job.Notes = "Could not process input file";
                        _client.UpdateJob(job).Wait();
                    }
                }
                finally
                {
                    IsFree = true;
                    var jobFolder = Path.Combine(_ocrStation.TempFolder, job.Id.ToString());
                    FileUtil.DeleteFolder(jobFolder);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        internal void Stop()
        {
            try
            {
                _process.Kill();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void Process_Exited(object sender, EventArgs e)
        {
            try
            {
                var p = sender as Process;
                if (p != null)
                {
                    p.CancelOutputRead();
                    p.CancelErrorRead();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            finally
            {
                IsFree = true;
            }
        }

        private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            _logger.Trace($"Tesseract ErrorDataReceived: {e.Data}");
        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            _logger.Trace($"Tesseract OutputDataReceived: {e.Data}");
        }
    }
}
