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
        public JobStateEnum State { get; set; }        
        public int DocumentPages { get; set; }
        public TimeSpan Duration { get; set; } = TimeSpan.Zero;
        public string Notes { get; set; } 
        public DateTime? ProcessedDate { get; set; }
    }
}
