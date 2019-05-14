using System;
using EasyMechBackend.DataAccessLayer.Entities;

namespace EasyMechBackend.ServiceLayer.DataTransferObject.DTOs
{
    public class GeplanterServiceDto
    {
        public long Id { get; set; }
        public string Bezeichnung { get; set; }

        public DateTime Beginn { get; set; }
        public DateTime Ende { get; set; }

        public long MaschinenId { get; set; }
        public long KundenId { get; set; }

        public ServiceDurchfuehrung ServiceDurchfuehrung { get; set; }
    }
}
