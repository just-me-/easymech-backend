using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.ServiceLayer.DataTransferObject
{
    public class FahrzeugtypDto : DtoBase
    {
        public long Id { get; set; }
        public string Fabrikat { get; set; }
        public string Motortyp { get; set; }
        public int Nutzlast { get; set; }
        public int Hubkraft { get; set; }
        public int Hubhoehe { get; set; }
        public int Eigengewicht { get; set; }
        public int Fahrzeughoehe { get; set; }
        public int Fahrzeuglaenge { get; set; }
        public int Fahrzeugbreite { get; set; }
        public int Pneugroesse { get; set; }
    }
}
