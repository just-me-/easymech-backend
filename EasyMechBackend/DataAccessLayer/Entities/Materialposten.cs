using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyMechBackend.DataAccessLayer.Entities
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

        public long ServiceId { get; set; }
        [ForeignKey(nameof(ServiceId))]
        [Required]
        public Service Service { get; set; }
    }
}
