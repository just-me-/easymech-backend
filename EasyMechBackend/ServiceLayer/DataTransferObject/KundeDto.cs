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
<<<<<<< HEAD
        public string Adresse { get; set; }
        public string PLZ { get; set; }
=======
        public int PLZ { get; set; }
>>>>>>> parent of cfb01e3... Merge branch 'master' of ssh://gitlab.dev.ifs.hsr.ch:45022/epj-2019-fs/easymech/easymech-backend
        public string Ort { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Notiz { get; set; }
        public bool IsActive { get; set; }
        public byte[] Timestamp { get; set; }
    }
}
