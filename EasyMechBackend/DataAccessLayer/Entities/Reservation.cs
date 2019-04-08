using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.DataAccessLayer
{
    public class Reservation
    {
        public string Standort { get; set; }

        public DateTime Startdatum { get; set; }

        public DateTime Rückgabedatum { get; set; }

        //Relationships
        // -------------------------------------------
        public FahrzeugUebergabe FahrzeugUebergabe { get; set; }

        public FahrzeugRuecknahme Fahrzeugruecknahme { get; set; }
        // -------------------------------------------

    }


}
