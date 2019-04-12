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
        [Required]
        public string Firma { get; set; }

        [MaxLength(128)]
        public string Vorname { get; set; }

        [MaxLength(128)]
        public string Nachname { get; set; }

        [MaxLength(128)]
        public string Adresse { get; set; }

        public string PLZ { get; set; }

        [MaxLength(128)]
        public string Ort { get; set; }

        [MaxLength(128)]
        public string Email { get; set; }

        [MaxLength(128)]
        public string Telefon { get; set; }

        public string Notiz { get; set; }

        public bool IsActive { get; set; }

        
        //Relationships
        // -------------------------------------------
        //public List<Maschine> Maschinen { get; set; }
        // -------------------------------------------


    }
}
