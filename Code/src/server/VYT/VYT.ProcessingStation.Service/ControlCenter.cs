using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYT.Models;

namespace VYT.ProcessingStation.Service
{
    public class ControlCenter
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private static volatile ControlCenter _uniqueInstance;
        private static readonly object _lockObject = new object();

        private ControlCenter()
        {
            try
            {
                JobManager = new JobManager(OcrStation);
                ProcessManager = new ProcessManager(OcrStation);                
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        public static ControlCenter GetInstance()
        {
            if (_uniqueInstance == null)
            {
                lock (_lockObject)
                {
                    if (_uniqueInstance == null)
                    {
                        _uniqueInstance = new ControlCenter();
                    }
                }
            }
            return _uniqueInstance;
        }
        public OcrStation OcrStation { get; } = new OcrStation();
        internal JobManager JobManager { get; }
        internal ProcessManager ProcessManager { get; }

        public void Start()
        {
            try
            {
                OcrStation.Load();
                OcrStation.Save();
                FileUtil.DeleteFolder(OcrStation.TempFolder);
                JobManager.Start();
                ProcessManager.Start();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        public void Stop()
        {
            try
            {
                JobManager.Stop();
                ProcessManager.Stop();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }
    }
}
