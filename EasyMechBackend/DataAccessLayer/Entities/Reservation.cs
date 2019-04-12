using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.DataAccessLayer
{

    [NotMapped] //Tabvle fehlt
    public class Reservation
    {
        [Key]
        public long Id { get; set; }


        public string Standort { get; set; }

        public DateTime Startdatum { get; set; }

        public DateTime Rückgabedatum { get; set; }

        //Relationships
        // -------------------------------------------
        public long MaschinenId { get; set; }

        [ForeignKey("MaschinenId")]
        public Maschine Maschine { get; set; }

        public long KundenId { get; set; }

        [ForeignKey("KundenId")]
        public Kunde Kunde { get; set; }

        public long UebergabeId { get; set; }

        [ForeignKey("UebergabeId")]
        public FahrzeugUebergabe FahrzeugUebergabe { get; set; }

        public long RuecknahmeId { get; set; }

        [ForeignKey("RuecknahmeId")]
        public FahrzeugRuecknahme Fahrzeugruecknahme { get; set; }
        // -------------------------------------------
        [Timestamp]
        public byte[] Timestamp { get; set; }

    }


}
