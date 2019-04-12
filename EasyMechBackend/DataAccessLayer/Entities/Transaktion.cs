using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.DataAccessLayer
{
    [NotMapped]
    public class Transaktion
    {
        [Key]
        public long Id { get; set; }

        public enum TransaktionsTyp { Verkauf, Einkauf};

        public double Preis { get; set; }

        public TransaktionsTyp Typ { get; set; }

        public DateTime Datum { get; set; }

        //Relationships
        // -------------------------------------------
        public long MaschinenId { get; set; }

        [ForeignKey("MaschinenId")]
        public Maschine Maschine { get; set; }

        public long KundenId { get; set; }

        [ForeignKey("KundenId")]
        public Kunde Kunde { get; set; }
        // -------------------------------------------

    }
}
