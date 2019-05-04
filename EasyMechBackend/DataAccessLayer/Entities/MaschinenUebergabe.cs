using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyMechBackend.DataAccessLayer.Entities
{
    [Table("MaschinenUebergabe", Schema = "public")]
    public class MaschinenUebergabe
    {
        [Key]
        public long Id { get; set; }        
        
        public DateTime Datum { get; set; }

        [ForeignKey(nameof(ReservationsId))]
        public Reservation Reservation { get; set; }
        public long ReservationsId { get; set; }

        public MaschinenRuecknahme Ruecknahme { get; set; }

    }


}
