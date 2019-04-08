using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.DataAccessLayer
{
    [Table("Kunden", Schema = "public")]
    public class Kunde
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(128)]
        public string Firma { get; set; }

        [Required, MaxLength(128)]
        public string Vorname { get; set; }

        [Required, MaxLength(128)]
        public string Nachname { get; set; }

        [Required]
        public int PLZ { get; set; }

        [Required, MaxLength(128)]
        public string Ort { get; set; }

        [Required, MaxLength(128)]
        public string Email { get; set; }

        [MaxLength(128)]
        public string Telefon { get; set; }

        public string Notiz { get; set; }

        public bool IsActive { get; set; }

        //Relationships
        // -------------------------------------------
        public List<Maschine> Maschinen { get; set; }
        // -------------------------------------------

        [Timestamp]
        public byte[] Timestamp { get; set; }

    }
}
