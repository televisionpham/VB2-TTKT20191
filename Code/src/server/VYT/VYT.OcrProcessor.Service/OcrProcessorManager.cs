using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VYT.OcrProcessor.Service
{
    public class OcrProcessorManager
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private static OcrProcessorManager _uniqueInstance;
        private static readonly object _lockObject = new object();

        private OcrProcessorManager()
        {

        }

        public static OcrProcessorManager GetInstance()
        {
            if (_uniqueInstance == null)
            {
                lock (_lockObject)
                {
                    if (_uniqueInstance == null)
                    {
                        _uniqueInstance = new OcrProcessorManager();
                    }
                }
            }

            return _uniqueInstance;
        }

        public void ProcessJob(string input, string languages, string output)
        {
            try
            {
                var execFile = ConfigurationManager.AppSettings["tesseract-path"];
                var process = new Process();
                process.EnableRaisingEvents = true;
                process.StartInfo.FileName = execFile;
                process.StartInfo.Arguments = $"{input} {output} -l {languages} pdf";                
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.OutputDataReceived += Process_OutputDataReceived;
                process.ErrorDataReceived += Process_ErrorDataReceived;
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();
                process.Exited += Process_Exited;
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
        }

        private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            _logger.Debug(e.Data);
        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            _logger.Trace(e.Data);
        }
    }
}
