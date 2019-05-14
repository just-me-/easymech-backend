using EasyMechBackend.Util;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyMechBackend.DataAccessLayer.Entities
{
    [Table("Kunden", Schema = "public")]
    public class Kunde : EntityWithValidate
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

        [MaxLength(128)]
        public string PLZ { get; set; }

        [MaxLength(128)]
        public string Ort { get; set; }

        [MaxLength(128)]
        public string Email { get; set; }

        [MaxLength(128)]
        public string Telefon { get; set; }

        public string Notiz { get; set; }

        [Required]
        public bool? IstAktiv { get; set; }

        public ICollection<Maschine> Maschinen { get; set; }
        public ICollection<Transaktion> Transaktionen { get; set; }
        public ICollection<Reservation> Reservationen { get; set; }
        public ICollection<Service> Services { get; set; }



        protected sealed override void FillRequiredProps()
        {
            if (Firma == null) Firma = "";
            if (IstAktiv == null) IstAktiv = true;
        }

        protected sealed override void ClipProps()
        {

            Firma = Firma.ClipToNChars(128);
            Vorname = Vorname.ClipToNChars(128);
            Nachname = Nachname.ClipToNChars(128);
            Adresse = Adresse.ClipToNChars(128);
            PLZ = PLZ.ClipToNChars(128);
            Ort = Ort.ClipToNChars(128);
            Email = Email.ClipToNChars(128);
            Telefon = Telefon.ClipToNChars(128);

        }
    }
}
