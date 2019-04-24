using EasyMechBackend.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
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


        //Relationships
        // -------------------------------------------
        public ICollection<Maschine> Maschinen { get; set; }
        public ICollection<Transaktion> Transaktionen { get; set; }
        public ICollection<Reservation> Reservationen { get; set; }
        // -------------------------------------------




        public void Validate()
        {
            ClipTo128Chars();
            FillRequiredFields();
        }

        private void FillRequiredFields()
        {
            if (Firma == null) Firma = "";
            if (IstAktiv == null) IstAktiv = true;
        }

        private void ClipTo128Chars()
        {

            //Option with reflection: Uses SetValue - ignores privacy - unsauber
            #region unusedOption
            //Code nur quickly testet
            /*
            PropertyInfo[] props = this.GetType().GetProperties();
            foreach (var prop in props)
            {

                if (prop.PropertyType != typeof(string)) continue;
                if (prop.Name == "Notiz") continue;


                string content = (string)prop.GetValue(this);
                prop.SetValue(this, content.ClipTo128Chars());
                
            }
            */
            #endregion


            //Option with DRY - weniger unsauber
            Firma = Firma.ClipTo128Chars();
            Vorname = Vorname.ClipTo128Chars();
            Nachname = Nachname.ClipTo128Chars();
            Adresse = Adresse.ClipTo128Chars();
            PLZ = PLZ.ClipTo128Chars();
            Ort = Ort.ClipTo128Chars();
            Email = Email.ClipTo128Chars();
            Telefon = Telefon.ClipTo128Chars();

        }
    }
}
