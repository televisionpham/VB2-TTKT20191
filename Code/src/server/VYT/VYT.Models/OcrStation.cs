using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VYT.Models
{
    public class OcrStation
    {
        public OcrStation()
        {
            TempFolder = Path.Combine(FileUtil.CurrentLocation, Constants.ProcessingStationTempFolder);
        }
        public int OcrProcessesCount { get; set; } = 1;
        public string TempFolder { get; set; }
        public int AskingInterval { get; set; } = 5;
    }
}
