using EasyMechBackend.Util;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyMechBackend.DataAccessLayer.Entities
{
    [Table("Arbeitsschritte", Schema = "public")]
    public class Arbeitsschritt : IValidatable
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
        public Service Service { get; set; }

        public void Validate()
        {
            Bezeichnung = Bezeichnung.ClipToNChars(256);
        }
    }
}
