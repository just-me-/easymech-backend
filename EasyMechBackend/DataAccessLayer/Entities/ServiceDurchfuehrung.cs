using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyMechBackend.DataAccessLayer.Entities
{
    [Table("ServiceDurchfuehrung", Schema = "public")]
    public class ServiceDurchfuehrung
    {
        [Key]
        public long Id { get; set; }        

        [ForeignKey(nameof(GeplanterServiceId))]
        [Required]
        public Service GeplanterService { get; set; }
        public long GeplanterServiceId { get; set; }

        public List<Materialposten> Materialposten { get; set; }
        public List<Arbeitsschritt> Arbeitsschritte { get; set; }

    }


}
