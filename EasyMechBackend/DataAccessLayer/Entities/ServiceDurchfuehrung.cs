﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.DataAccessLayer
{
    [Table("ServiceDurchfuehrung", Schema = "public")]
    public class ServiceDurchfuehrung
    {
        [Key]
        public long Id { get; set; }        

        //Relationships
        // -------------------------------------------
        public GeplanterService GeplanterService { get; set; }

        public List<Materialposten> Materialposten { get; set; }


        // -------------------------------------------

        [Timestamp]
        public byte[] Timestamp { get; set; }

    }


}