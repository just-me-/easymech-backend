﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EasyMechBackend.Util;

namespace EasyMechBackend.DataAccessLayer.Entities
{
    [Table("Reservationen", Schema = "public")]
    public class Reservation : IValidatable
    {
        [Key] public long Id { get; set; }

        [MaxLength(256)] public string Standort { get; set; }

        public DateTime? Startdatum { get; set; }

        public DateTime? Enddatum { get; set; }

        [ForeignKey(nameof(MaschinenId))]
        [Required]
        public Maschine Maschine { get; set; }

        public long MaschinenId { get; set; }

        [ForeignKey(nameof(KundenId))] public Kunde Kunde { get; set; }
        public long? KundenId { get; set; }

        public virtual MaschinenUebergabe Uebergabe { get; set; }
        public virtual MaschinenRuecknahme Ruecknahme { get; set; }
        
        public void Validate()
        {
            Standort = Standort.ClipToNChars(256);
        }
    }
}
