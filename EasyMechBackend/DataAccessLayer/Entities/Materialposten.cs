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

        public long ServiceDurchfuehrungId { get; set; }
        [ForeignKey(nameof(ServiceDurchfuehrungId))]
        [Required]
        public ServiceDurchfuehrung ServiceDurchfuehrung { get; set; }

    }


}
