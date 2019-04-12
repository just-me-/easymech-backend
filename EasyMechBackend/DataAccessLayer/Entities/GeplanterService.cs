using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.DataAccessLayer
{
    public class GeplanterService
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(1028)]
        public string Bezeichnung { get; set; }

        public DateTime Beginn { get; set; }

        public DateTime Ende { get; set; }

        //Relationships
        // -------------------------------------------
        public long MaschinenId { get; set; }

        [ForeignKey("MaschinenId")]
        public Maschine Maschine { get; set; }

        public long KundenId { get; set; }

        [ForeignKey("KundenId")]
        public Kunde Kunde { get; set; }

        public long ServiceDurchfuehrungsId { get; set; }

        [ForeignKey("ServiceDurchfuehrungsId")]
        public ServiceDurchfuehrung ServiceDurchfuehrung { get; set; }
        // -------------------------------------------

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }


}
