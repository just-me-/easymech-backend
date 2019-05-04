using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyMechBackend.DataAccessLayer.Entities
{
    [Table("Reservationen", Schema = "public")]
    public class Reservation
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(256)]
        public string Standort { get; set; }

        public DateTime Startdatum { get; set; }

        public DateTime Enddatum { get; set; }


        [ForeignKey(nameof(MaschinenId))]
        [Required]
        public Maschine Maschine { get; set; }
        public long MaschinenId { get; set; }



        [ForeignKey(nameof(KundenId))]
        public Kunde Kunde { get; set; }
        public long? KundenId { get; set; }


        public MaschinenUebergabe Uebergabe { get; set; }


        
        //Assistant Property !! Todo: See if this works out with lazy loading or such
        [NotMapped] public MaschinenRuecknahme Ruecknahme => Uebergabe.Ruecknahme;

    }


}
