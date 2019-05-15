using EasyMechBackend.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using static EasyMechBackend.Common.EnumHelper;

namespace EasyMechBackend.Common.DataTransferObject.DTOs
{
    public class ServiceDto
    {
        public long Id { get; set; }
        public string Bezeichnung { get; set; }

        public DateTime Beginn { get; set; }
        public DateTime Ende { get; set; }

        public ServiceState Status { get; set; }
        public long MaschinenId { get; set; }
        public long KundenId { get; set; }

        public List<MaterialpostenDto> Materialposten { get; set; }
        public List<ArbeitsschrittDto> Arbeitsschritte { get; set; }
    }
}
