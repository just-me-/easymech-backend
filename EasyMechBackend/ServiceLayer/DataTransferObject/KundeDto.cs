using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.ServiceLayer.DataTransferObject
{
    public class KundeDto
    {
        public long Id { get; set; }
        public string Firma { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public int PLZ { get; set; }
        public string Ort { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Notiz { get; set; }
        public bool IsActive { get; set; }
        public byte[] Timestamp { get; set; }
    }
}
