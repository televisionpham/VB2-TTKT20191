using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VYT.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Languages { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string InputFile { get; set; }
        public JobStateEnum State { get; set; }        
        public int DocumentPages { get; set; }
        public TimeSpan Duration { get; set; }
        public string Notes { get; set; }
        public IList<string> OutputDocuments { get; } = new List<string>();
        public OcrSettings OcrSettings { get; } = new OcrSettings();
    }
}
