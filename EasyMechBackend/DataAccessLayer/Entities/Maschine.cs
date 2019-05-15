using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EasyMechBackend.Util;

namespace EasyMechBackend.DataAccessLayer.Entities
{
    [Table("Maschine", Schema = "public")]
    public class Maschine : IValidatable
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

        [ForeignKey(nameof(BesitzerId))]
        [Required]
        public Kunde Besitzer { get; set; }
        public long BesitzerId { get; set; }

        [ForeignKey(nameof(MaschinentypId))]
        [Required]
        public Maschinentyp Maschinentyp { get; set; }
        public long MaschinentypId { get; set; }

        public ICollection<Transaktion> Transaktionen { get; set; }
        public ICollection<Reservation> Reservationen { get; set; }
        public ICollection<Service> Services { get; set; }


        public void Validate()
        {
            FillRequiredProps();
            ClipProps();
        }

        private void FillRequiredProps()
        {
            if (IstAktiv == null) IstAktiv = true;
        }

        private void ClipProps()
        {
            Seriennummer = Seriennummer.ClipToNChars(128);
            Mastnummer = Mastnummer.ClipToNChars(128);
            Motorennummer = Motorennummer.ClipToNChars(128);

        }
    }
}
