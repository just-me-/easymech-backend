using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.DataAccessLayer
{

    public enum TransaktionsTyp { Verkauf = 0, Einkauf = 1 };


    [Table("Transaktionen", Schema = "public")]
    public class Transaktion
    {
        


        [Key]
        public long Id { get; set; }

        [Required]
        public double Preis { get; set; }

        [Required]
        public TransaktionsTyp Typ { get; set; }

        public DateTime? Datum { get; set; }

        //Relationships
        // -------------------------------------------
        public long MaschinenId { get; set; }
        [ForeignKey("MaschinenId")]
        [Required]
        public Maschine Maschine { get; set; }


        public long KundenId { get; set; }
        [ForeignKey("KundenId")]
        public Kunde Kunde { get; set; }
        // -------------------------------------------

    }
}
