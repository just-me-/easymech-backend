using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.DataAccessLayer
{
    [Table("FahrzeugUebergabe", Schema = "public")]
    public class FahrzeugUebergabe
    {
        [Key]
        public long Id { get; set; }        
        
        public DateTime Datum { get; set; }

        //Relationships
        // -------------------------------------------
        [ForeignKey(nameof(ReservationsId))]
        public Reservation Reservation { get; set; }
        public long ReservationsId { get; set; }

        //NavProp
        public FahrzeugRuecknahme Ruecknahme { get; set; }
        // -------------------------------------------

    }


}
