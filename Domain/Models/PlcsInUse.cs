using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PlcsInUse
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string AddressPlc { get; set; }
        public required string Type { get; set; }
        public string? Value { get; set; }

    }
}
