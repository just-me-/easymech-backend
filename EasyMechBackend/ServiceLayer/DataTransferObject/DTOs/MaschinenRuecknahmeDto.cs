using System;

namespace EasyMechBackend.ServiceLayer.DataTransferObject.DTOs
{
    public class MaschinenRuecknahmeDto
    {
        //public long Id { get; set; }
        public DateTime? Datum { get; set; }
        public string Notiz { get; set; }
    }
}
