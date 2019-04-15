﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.DataAccessLayer
{
    [Table("Arbeitsschritt", Schema = "public")]
    [NotMapped]
    public class Arbeitsschritt
    {
        [Key]
        public long Id { get; set; }        
        
        public string Bezeichnung { get; set; }

        public double Stundenansatz { get; set; }

        public double Arbeitsstunden { get; set; }

        //Relationships
        // -------------------------------------------
        public long ServiceDurchfuehrungId { get; set; }

        [ForeignKey("ServiceDurchfuehrungId")]
        public ServiceDurchfuehrung ServiceDurchfuehrung { get; set; }
        // -------------------------------------------

    }


}
