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

        public void Load()
        {
            var configFile = Path.Combine(FileUtil.CurrentLocation, Constants.ConfigFolder, "ocr-station.json");
            if (!File.Exists(configFile))
            {
                return;
            }
            var data = ObjectSerializer.JSONDeserilizeFromFile<OcrStation>(configFile);
            CopyFrom(data);
        }

        public void Save()
        {
            var configFile = Path.Combine(FileUtil.CurrentLocation, Constants.ConfigFolder, "ocr-station.json");
            ObjectSerializer.JSONSerializeToFile(this, configFile);
        }

        public void CopyFrom(OcrStation data)
        {
            this.TempFolder = data.TempFolder;
            this.OcrProcessesCount = data.OcrProcessesCount;
            this.AskingInterval = data.AskingInterval;
        }
    }
}
