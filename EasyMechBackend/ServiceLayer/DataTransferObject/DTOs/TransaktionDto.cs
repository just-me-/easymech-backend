using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static EasyMechBackend.DataAccessLayer.Transaktion;

namespace EasyMechBackend.ServiceLayer.DataTransferObject
{
    public class TransaktionDto : DtoBase
    {
        public long Id { get; set; }
        public double Preis { get; set; }
        public TransaktionsTyp Typ { get; set; }
        public DateTime? Datum { get; set; }
        public long MaschinenId { get; set; }
        public long? KundenId { get; set; }
    }
}
