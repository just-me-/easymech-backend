using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.DataAccessLayer
{
    [Table("MaschinenUebergabe", Schema = "public")]
    public class MaschinenUebergabe
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
        public MaschinenRuecknahme Ruecknahme { get; set; }
        // -------------------------------------------

    }


}
