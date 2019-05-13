using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyMechBackend.DataAccessLayer.Entities
{
    [Table("GeplanterService", Schema = "public")]
    public class GeplanterService : EntityWithValidate
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(128)]
        public string Bezeichnung { get; set; }

        public DateTime Beginn { get; set; }

        public DateTime Ende { get; set; }
        

        [ForeignKey(nameof(MaschinenId))]
        [Required]
        public Maschine Maschine { get; set; }
        public long MaschinenId { get; set; }


        [ForeignKey(nameof(KundenId))]
        public Kunde Kunde { get; set; }
        public long KundenId { get; set; }

        
        public ServiceDurchfuehrung ServiceDurchfuehrung { get; set; }

        protected override void ClipProps()
        {
            throw new NotImplementedException();
        }

        protected override void FillRequiredProps()
        {
            throw new NotImplementedException();
        }
    }


}
