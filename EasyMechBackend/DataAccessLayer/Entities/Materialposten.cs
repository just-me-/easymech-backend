using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.DataAccessLayer
{
    [Table("Materialposten", Schema = "public")]
    public class Materialposten
    {
        [Key]
        public long Id { get; set; }        
        
        [Required]
        public double Stueckpreis { get; set; }

        [Required]
        public int Anzahl { get; set; }

        [MaxLength(256)]
        public string Bezeichnung { get; set; }

        //Relationships
        // -------------------------------------------
        public long ServiceDurchfuehrungId { get; set; }
        [ForeignKey(nameof(ServiceDurchfuehrungId))]
        [Required]
        public ServiceDurchfuehrung ServiceDurchfuehrung { get; set; }
        // -------------------------------------------


    }


}
