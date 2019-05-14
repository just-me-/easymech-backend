using EasyMechBackend.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static EasyMechBackend.Common.EnumHelper;

namespace EasyMechBackend.DataAccessLayer.Entities
{
    [Table("GeplanterService", Schema = "public")]
    public class Service : IValidatable
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(128)]
        public string Bezeichnung { get; set; }

        public DateTime Beginn { get; set; }
        public DateTime Ende { get; set; }

        public ServiceState Status { get; set; }

        [ForeignKey(nameof(MaschinenId))]
        [Required]
        public Maschine Maschine { get; set; }
        public long MaschinenId { get; set; }

        [ForeignKey(nameof(KundenId))]
        public Kunde Kunde { get; set; }
        public long KundenId { get; set; }

        public List<Materialposten> Materialposten { get; set; }
        public List<Arbeitsschritt> Arbeitsschritte { get; set; }

        public void Validate()
        {
            Bezeichnung = Bezeichnung.ClipToNChars(128);
        }
    }
}
