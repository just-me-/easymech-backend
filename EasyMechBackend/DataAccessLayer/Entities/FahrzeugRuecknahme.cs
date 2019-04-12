using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.DataAccessLayer
{
    [Table("FahrzeugRuecknahme", Schema = "public")]
    public class FahrzeugRuecknahme
    {
        [Key]
        public long Id { get; set; }        
        
        public DateTime Datum { get; set; }

        //Relationships
        // -------------------------------------------
        public Reservation Reservation { get; set; }
        // -------------------------------------------

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }


}
