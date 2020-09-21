using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VYT.ProcessingStation.Service
{
    public class ProcessManager
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private static ProcessManager _uniqueInstance;
        private static readonly object _lockObject = new object();
        private List<OcrProcessor> _processors = new List<OcrProcessor>();

        private ProcessManager()
        {

        }

        public static ProcessManager GetInstance()
        {
            if (_uniqueInstance == null)
            {
                lock (_lockObject)
                {
                    if (_uniqueInstance == null)
                    {
                        _uniqueInstance = new ProcessManager();
                    }
                }
            }

            return _uniqueInstance;
        }
    }
}
