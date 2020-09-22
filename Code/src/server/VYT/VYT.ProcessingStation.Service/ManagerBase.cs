using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYT.ApplicationServiceClient;
using VYT.Models;

namespace VYT.ProcessingStation.Service
{
    internal abstract class ManagerBase
    {
        protected static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        protected readonly OcrStation _ocrStation;
        protected static bool _isRunning = false;
        protected WebApiClient _client;

        public ManagerBase(OcrStation ocrStation)
        {
            this._ocrStation = ocrStation;
            var baseAddress = ConfigurationManager.AppSettings["web-api-address"];
            _client = new WebApiClient(baseAddress);
        }

        public abstract void Start();
        public abstract void Stop();        
    }
}
