using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYT.Models;

namespace VYT.ProcessingStation.Service
{
    internal class ProcessManager : ManagerBase
    {        
        private List<OcrProcessor> _processors = new List<OcrProcessor>();

        public ProcessManager(OcrStation ocrStation) : base(ocrStation)
        {
        }

        public override void Start()
        {
            try
            {
                _processors.Clear();
                for (int i = 0; i < _ocrStation.OcrProcessesCount; i++)
                {
                    _processors.Add(new OcrProcessor(_ocrStation));
                }
                
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        public override void Stop()
        {
            try
            {
                foreach (var processor in _processors)
                {
                    processor.Stop();
                }
                _processors.Clear();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        public OcrProcessor GetFreeProcess()
        {
            return _processors.FirstOrDefault(x => x.IsFree);
        }
    }
}
