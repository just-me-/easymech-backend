using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.DataAccessLayer
{
    [Table("Arbeitsschritt", Schema = "public")]
    public class Arbeitsschritt
    {
        [Key]
        public long Id { get; set; }        
        
        [MaxLength(256)]
        public string Bezeichnung { get; set; }

        public double? Stundenansatz { get; set; }

        public double? Arbeitsstunden { get; set; }

        //Relationships
        // -------------------------------------------
        public long ServiceDurchfuehrungId { get; set; }
        [ForeignKey(nameof(ServiceDurchfuehrungId))]
        [Required]
        public ServiceDurchfuehrung ServiceDurchfuehrung { get; set; }
        // -------------------------------------------

    }


}
