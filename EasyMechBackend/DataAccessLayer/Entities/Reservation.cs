using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.DataAccessLayer
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


        //Relationships
        // -------------------------------------------

        [ForeignKey("MaschinenId")]
        [Required]
        public Maschine Maschine { get; set; }
        public long MaschinenId { get; set; }



        [ForeignKey("KundenId")]
        public Kunde Kunde { get; set; }
        public long? KundenId { get; set; }


        // ------------Navigation Properties----------
        public FahrzeugUebergabe Uebergabe { get; set; }
        // -------------------------------------------


        //
        //Assistant Property !! Lazy Loading not active !! Will not work. Ka warum ich es hinschreib überhaupt.
        [NotMapped]
        public FahrzeugRuecknahme Ruecknahme { get => Uebergabe.Ruecknahme; }

    }


}
