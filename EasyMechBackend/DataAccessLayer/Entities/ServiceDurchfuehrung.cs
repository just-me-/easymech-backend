using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.DataAccessLayer
{
    [Table("ServiceDurchfuehrung", Schema = "public")]
    [NotMapped]
    public class ServiceDurchfuehrung
    {
        [Key]
        public long Id { get; set; }        

        //Relationships
        // -------------------------------------------
        public long GeplanterServiceId { get; set; }

        [ForeignKey("GeplanterServiceId")]
        public GeplanterService GeplanterService { get; set; }

        public List<Materialposten> Materialposten { get; set; }

        public List<Arbeitsschritt> Arbeitsschritte { get; set; }
        // -------------------------------------------


    }


}
