using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace EasyMechBackend.DataAccessLayer
{
    [NotMapped]
    public class Mitarbeiter
    {
        [Key]
        public long Id { get; set; }        
        
        [MaxLength(128)]
        public string Name { get; set; }

        [MaxLength(128)]
        public string Vorname { get; set; }

        //Relationships
        // -------------------------------------------
        public long MaschinenId { get; set; }

        [ForeignKey("MaschinenId")]
        public List<Maschine> Maschinen { get; set; }
        // -------------------------------------------

    }
}
