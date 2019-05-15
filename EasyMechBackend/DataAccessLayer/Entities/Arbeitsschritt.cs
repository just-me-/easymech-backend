using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyMechBackend.DataAccessLayer.Entities
{
    [Table("Arbeitsschritt", Schema = "public")]
    public class Arbeitsschritt
    {
        [Key]
        public long Id { get; set; }        
        
        [MaxLength(256)]
        public string Bezeichnung { get; set; }

        public double? Stundenansatz { get; set; }
        public double? Arbeitsstunden { get; set; }

        public long ServiceId { get; set; }
        [ForeignKey(nameof(ServiceId))]
        [Required]
        public ServiceDurchfuehrung Service { get; set; }
    }
}
