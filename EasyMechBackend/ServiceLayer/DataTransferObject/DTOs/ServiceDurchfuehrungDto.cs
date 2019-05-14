using System;
using EasyMechBackend.DataAccessLayer.Entities;

namespace EasyMechBackend.ServiceLayer.DataTransferObject.DTOs
{
    public class ServiceDurchfuehrungDto
    {
        public long Id { get; set; }
        public long GeplanterServiceId { get; set; }
    }
}
