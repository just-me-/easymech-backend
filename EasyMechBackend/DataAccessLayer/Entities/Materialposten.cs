using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.DataAccessLayer
{
    [Table("Materialposten", Schema = "public")]
    [NotMapped]
    public class Materialposten
    {
        [Key]
        public long Id { get; set; }        
        
        public double Stueckpreis { get; set; }

        public int Anzahl { get; set; }

        //Relationships
        // -------------------------------------------
        public ServiceDurchfuehrung ServiceDurchfuehrung { get; set; }
        // -------------------------------------------

        [Timestamp]
        public byte[] Timestamp { get; set; }

    }


}
