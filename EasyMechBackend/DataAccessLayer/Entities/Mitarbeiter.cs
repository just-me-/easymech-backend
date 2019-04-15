using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace EasyMechBackend.DataAccessLayer
{
    [Table("Mitarbeiter", Schema = "public")]
    [NotMapped]
    public class Mitarbeiter
    {
        [Key]
        public long Id { get; set; }        
        
        [MaxLength(128)]
        public string Name { get; set; }

        [MaxLength(128)]
        public string Vorname { get; set; }

        //Relationships
        // -------------------------------------------
        public List<Reservation> Reservationen { get; set; }

        public List<Transaktion> Transaktionen { get; set; }

        public List<GeplanterService> GeplanteServices { get; set; }
        // -------------------------------------------

    }
}
