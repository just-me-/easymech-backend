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

        public int? Betriebsdauer { get; set; }

        public int? Jahrgang { get; set; }

        public string Notiz { get; set; }

        [Required]
        public bool? IstAktiv { get; set; }


        //Relationships
        // -------------------------------------------
     
        [ForeignKey(nameof(BesitzerId))]
        [Required]
        public Kunde Besitzer { get; set; }
        public long BesitzerId { get; set; }


        [ForeignKey(nameof(MaschinentypId))]
        [Required]
        public Maschinentyp Maschinentyp { get; set; }
        public long MaschinentypId { get; set; }

        // -------------------------------------------

        public ICollection<Transaktion> Transaktionen { get; set; }
        public ICollection<Reservation> Reservationen { get; set; }
        public ICollection<GeplanterService> Services { get; set; }

        // -------------------------------------------

        public void Validate()
        {
            ClipTo128Chars();
            FillRequiredFields();
        }

        private void FillRequiredFields()
        {
            if (IstAktiv == null) IstAktiv = true;
        }

        private void ClipTo128Chars()
        {
            Seriennummer = Seriennummer.ClipTo128Chars();
            Mastnummer = Mastnummer.ClipTo128Chars();
            Motorennummer = Motorennummer.ClipTo128Chars();

        }
    }
}
