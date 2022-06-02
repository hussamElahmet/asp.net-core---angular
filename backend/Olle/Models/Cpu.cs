using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Olle.Models
{
    public class CPU
    {
        public string checksum { set; get; }
        public List<data> data { set; get; }

    }
    public class time
    {
        public string user { set; get; }
        public string nice { set; get; }
        public string sys { set; get; }
        public string idle { set; get; }
        public string irq { set; get; }
    }

    public class data
    {
        public string model { set; get; }
        public string speed { set; get; }
        public time times { set; get; }
    }
}
