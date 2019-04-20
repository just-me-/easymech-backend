using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.ServiceLayer.DataTransferObject
{
    public class MaschineDto : DtoBase
    {
        public long Id { get; set; }
        public string Seriennummer { get; set; }
        public string Mastnummer { get; set; }
        public string Motorennummer { get; set; }
        public int Betriebsdauer { get; set; }
        public int Jahrgang { get; set; }
        public string Notiz { get; set; }
        public bool? IsActive { get; set; }
    }
}
