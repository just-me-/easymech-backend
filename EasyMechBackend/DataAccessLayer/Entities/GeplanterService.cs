using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.DataAccessLayer
{
    public class GeplanterService : Aktion
    {
        public string Bezeichnung { get; set; }

        public DateTime Beginn { get; set; }

        public DateTime Ende { get; set; }

        //Relationships
        // -------------------------------------------
        public ServiceDurchfuehrung ServiceDurchfuehrung { get; set; }
        // -------------------------------------------
    }


}
