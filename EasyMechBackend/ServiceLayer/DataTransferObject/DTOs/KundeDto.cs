using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.ServiceLayer.DataTransferObject
{
    public class KundeDto : DtoBase
    {
        public long Id { get; set; }
        public string Firma { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Adresse { get; set; }
        public string PLZ { get; set; }
        public string Ort { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Notiz { get; set; }
        public bool? IsActive { get; set; }
    }
}
