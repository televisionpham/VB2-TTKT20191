using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VYT.Models
{
    public enum JobStateEnum
    {
        Waiting = 0,
        Processing = 1,
        Processed = 2,
        Error = -1
    }
}
