using EasyMechBackend.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using static EasyMechBackend.Common.EnumHelper;

namespace EasyMechBackend.Common.DataTransferObject.DTOs
{
    public class ArbeitsschrittDto
    {
        public long Id { get; set; }
        public string Bezeichnung { get; set; }
        public double? Stundenansatz { get; set; }
        public double? Arbeitsstunden { get; set; }
        public long ServiceId { get; set; }
    }
}
