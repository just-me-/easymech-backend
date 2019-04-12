using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

/*
// Datenstruktur anders gewählt

namespace EasyMechBackend.DataAccessLayer
{
    [NotMapped]
    public class Aktion
    {
        [Key]
        public long Id { get; set; }        
        
        public Maschine Maschine { get; set; }

        public Kunde Kunde { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

    }
}
*/