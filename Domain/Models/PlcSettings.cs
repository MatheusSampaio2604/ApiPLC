using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PlcSettings
    {
        public required string Ip1 { get; set; }
        public required string CpuType { get; set; }
        public required short Rack { get; set; }
        public required short Slot { get; set; }
        public required string Driver { get; set; }
    }
}
