using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using VYT.ProcessingStation.Service;

namespace VYT.ProcessingStation
{
    public partial class MyService : ServiceBase
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public MyService()
        {
            InitializeComponent();
        }

        internal void Start()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                ControlCenter.GetInstance().Start();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        protected override void OnStop()
        {
            try
            {
                ControlCenter.GetInstance().Stop();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }
    }
}
