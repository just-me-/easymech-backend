using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.DataAccessLayer
{
    [Table("MaschinenRuecknahme", Schema = "public")]
    public class MaschinenRuecknahme
    {
        [Key]
        public long Id { get; set; }        
        
        public DateTime Datum { get; set; }

        //Relationships
        // -------------------------------------------
        [ForeignKey(nameof(MaschinenUebergabeId))]
        public MaschinenUebergabe MaschinenUebergabe { get; set; }
        public long MaschinenUebergabeId { get; set; }
        // -------------------------------------------

    }


}
