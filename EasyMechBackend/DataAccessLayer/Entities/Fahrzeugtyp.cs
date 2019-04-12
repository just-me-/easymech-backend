using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.DataAccessLayer
{
    [Table("Fahrzeugtyp", Schema = "public")]
    [NotMapped]
    public class Fahrzeugtyp
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(128)]
        public string Fabrikat { get; set; }

        [MaxLength(128)]
        public string Motortyp { get; set; }

        public int Nutzlast { get; set; }

        public int Hubkraft { get; set; }

        public int Hubhöhe { get; set; }

        public int Eigengewicht { get; set; }

        public int Jahrgang { get; set; }

        public int Fahrzeughöhe { get; set; }

        public int Fahrzeuglänge { get; set; }

        public int Fahrzeugbreite { get; set; }

        public int Pneugrösse { get; set; }

        //Relationships
        // -------------------------------------------
        public List<Maschine> Maschinen { get; set; }
        // -------------------------------------------


    }
}
