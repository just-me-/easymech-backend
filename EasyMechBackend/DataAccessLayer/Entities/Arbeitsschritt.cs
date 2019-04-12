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
        
        public string Bezeichnung { get; set; }

        public double Stundenansatz { get; set; }

        public double Arbeitsstunden { get; set; }

        //Relationships
        // -------------------------------------------
        public ServiceDurchfuehrung ServiceDurchfuehrung { get; set; }
        // -------------------------------------------

        [Timestamp]
        public byte[] Timestamp { get; set; }

    }


}
