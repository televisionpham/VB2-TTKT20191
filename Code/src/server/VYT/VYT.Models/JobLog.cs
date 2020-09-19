using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VYT.Models
{
    public class JobLog
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public DateTime CreatedDate { get; set; } = DateTime.Now;        
        public JobStateEnum State { get; set; }
        public int DocumentPages { get; set; }
        public TimeSpan Duration { get; set; }
        public string Notes { get; set; }

        public void CopyFrom(Job job)
        {
            this.Name = job.Name;
            this.CreatedDate = job.CreatedDate;
            this.State = job.State;
            this.DocumentPages = job.DocumentPages;
            this.Duration = job.Duration;
            this.Notes = job.Notes;
        }
    }
}
