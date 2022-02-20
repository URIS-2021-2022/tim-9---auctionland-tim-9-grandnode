using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZalbaService.Models.Zalba
{
    public class ZalbaDto
    {
        public Guid TipZalbe { get; set; }
        public DateTime DatumZalbe { get; set; }
        public Guid PodnosilacZalbe { get; set; }
        public string Razlog { get; set; }
        public string Obrazlozenje { get; set; }
        public DateTime DatumResenja { get; set; }
        public string BrojResenja { get; set; }
        public Guid StatusZalbe { get; set; }
        public string BrojOdluke { get; set; }
        public Guid Radnja { get; set; }
    }
}
