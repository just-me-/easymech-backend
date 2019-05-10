using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EasyMechBackend.Util;

namespace EasyMechBackend.DataAccessLayer.Entities
{
    [Table("Maschinentyp", Schema = "public")]
    public class Maschinentyp : EntityWithValidate
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(128)]
        [Required]
        public string Fabrikat { get; set; }

        [MaxLength(128)]
        public string Motortyp { get; set; }

        public int? Nutzlast { get; set; }

        public int? Hubkraft { get; set; }

        public int? Hubhoehe { get; set; }

        public int? Eigengewicht { get; set; }

        public int? Maschinenhoehe { get; set; }

        public int? Maschinenlaenge { get; set; }

        public int? Maschinenbreite { get; set; }

        public int? Pneugroesse { get; set; }

        public ICollection<Maschine> Maschinen { get; set; }


        protected sealed override void ClipProps()
        {
            Fabrikat = Fabrikat.ClipToNChars(128);
            Motortyp = Motortyp.ClipToNChars(128);
        }

        protected sealed override void FillRequiredProps()
        {
            Fabrikat = "";
        }
    }
}
