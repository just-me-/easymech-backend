using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyMechBackend.DataAccessLayer.Entities
{
    [Table("Transaktionen", Schema = "public")]
    public class Transaktion
    {

        public enum TransaktionsTyp { Verkauf = 0, Einkauf = 1 }

        [Key]
        public long Id { get; set; }

        [Required]
        public double Preis { get; set; }

        [Required]
        public TransaktionsTyp Typ { get; set; }

        public DateTime? Datum { get; set; }

        public long MaschinenId { get; set; }
        [ForeignKey(nameof(MaschinenId))]
        [Required]
        public Maschine Maschine { get; set; }

        public long? KundenId { get; set; }
        [ForeignKey(nameof(KundenId))]
        public Kunde Kunde { get; set; }

    }
}
