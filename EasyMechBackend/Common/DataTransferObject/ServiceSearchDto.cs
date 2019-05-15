using System;
using static EasyMechBackend.Common.EnumHelper;

namespace EasyMechBackend.Common
{
    public class ServiceSearchDto
    {
        public DateTime? Von { get; set; }
        public DateTime? Bis { get; set; }
        public long? MaschinenId { get; set; }
        public long? MaschinentypId { get; set; }
        public long? KundenId { get; set; }
        public ServiceState? Status { get; set; }
    }
}
