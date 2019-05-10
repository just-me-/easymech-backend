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
        
        public DateTime? Datum { get; set; }

        public string Notiz { get; set; }

        [ForeignKey(nameof(ReservationsId))]
        [Required]
        public Reservation Reservation { get; set; }
        public long ReservationsId { get; set; }

    }


}
