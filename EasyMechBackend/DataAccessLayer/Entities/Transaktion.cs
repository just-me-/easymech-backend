using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.DataAccessLayer
{
    public class Transaktion : Aktion
    {
        public double Preis { get; set; }

        public DateTime Datum { get; set; }
    }

    public class Verkauf : Transaktion
    { }

    public class Einkauf : Transaktion
    { }
}
