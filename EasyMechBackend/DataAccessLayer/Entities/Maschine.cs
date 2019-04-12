using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.DataAccessLayer
{
    [Table("Maschine", Schema = "public")]
    [NotMapped]
    public class Maschine
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(128)]
        public string Seriennummer { get; set; }

        [MaxLength(128)]
        public string Mastnummer { get; set; }

        [MaxLength(128)]
        public string Motorennummer { get; set; }

        public int Bertriebsdauer { get; set; }

        [Required]
        public bool IstAktiv { get; set; }

        //Relationships
        // -------------------------------------------
        [Required]
        public Fahrzeugtyp Typ { get; set; }

        public Kunde Besitzer { get; set; }
        // -------------------------------------------

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
