using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using EasyMechBackend.Util;

namespace EasyMechBackend.DataAccessLayer
{
    [Table("Maschinentyp", Schema = "public")]
    public class Maschinentyp
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(128)]
        public string Fabrikat { get; set; }

        [MaxLength(128)]
        public string Motortyp { get; set; }

        public int Nutzlast { get; set; }

        public int Hubkraft { get; set; }

        public int Hubhoehe { get; set; }

        public int Eigengewicht { get; set; }

        public int Maschinenhoehe { get; set; }

        public int Maschinenlaenge { get; set; }

        public int Maschinenbreite { get; set; }

        public int Pneugroesse { get; set; }

        //Relationships
        // -------------------------------------------
        public ICollection<Maschine> Maschinen { get; set; }
        // -------------------------------------------

        public void Validate()
        {
            ClipTo128Chars();
        }

        private void ClipTo128Chars()
        {
            Fabrikat = Fabrikat.ClipTo128Chars();
            Motortyp = Motortyp.ClipTo128Chars();
        }
    }
}
