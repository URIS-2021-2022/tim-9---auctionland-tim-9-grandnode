using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicnostService.Models
{
    public class Message
    {
        public string ServiceName { get; set; }
        public string Method { get; set; }
        public string Information { get; set; }
        public string Error { get; set; }
    }
}
