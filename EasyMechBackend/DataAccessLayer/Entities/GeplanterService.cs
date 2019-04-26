﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.DataAccessLayer
{
    [Table("GeplanterService", Schema = "public")]
    public class GeplanterService
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(128)]
        public string Bezeichnung { get; set; }

        public DateTime Beginn { get; set; }

        public DateTime Ende { get; set; }

        //Relationships
        // -------------------------------------------
        

        [ForeignKey("MaschinenId")]
        [Required]
        public Maschine Maschine { get; set; }
        public long MaschinenId { get; set; }


        

        [ForeignKey("KundenId")]
        public Kunde Kunde { get; set; }
        public long KundenId { get; set; }

        
        //NavProp
        public ServiceDurchfuehrung ServiceDurchfuehrung { get; set; }
        // -------------------------------------------

    }


}
