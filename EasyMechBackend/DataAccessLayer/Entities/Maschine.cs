using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using EasyMechBackend.Util;

namespace EasyMechBackend.DataAccessLayer
{
    [Table("Maschine", Schema = "public")]
    public class Maschine
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(128)]
        public string Seriennummer { get; set; }

        [MaxLength(128)]
        public string Mastnummer { get; set; }

        [MaxLength(128)]
        public string Motorennummer { get; set; }

        public int Betriebsdauer { get; set; }

        [Required]
        public bool? IsActive { get; set; }

        //Relationships
        // -------------------------------------------
        [Required]
        public Fahrzeugtyp Typ { get; set; }

        public Kunde Besitzer { get; set; }
        // -------------------------------------------

        public void Validate()
        {
            ClipTo128Chars();
            FillRequiredFields();
        }

        private void FillRequiredFields()
        {
            if (IsActive == null) IsActive = true;
        }

        private void ClipTo128Chars()
        {
            Seriennummer = Seriennummer.ClipTo128Chars();
            Mastnummer = Mastnummer.ClipTo128Chars();
            Motorennummer = Motorennummer.ClipTo128Chars();

        }
    }
}
