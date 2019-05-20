using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyMechBackend.DataAccessLayer.Entities
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

        public List<Reservation> Reservationen { get; set; }

        public List<Transaktion> Transaktionen { get; set; }

        public List<Service> Services { get; set; }
    }
}
