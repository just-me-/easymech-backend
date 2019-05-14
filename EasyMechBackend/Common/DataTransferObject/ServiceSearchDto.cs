using System;

namespace EasyMechBackend.Common.DataTransferObject
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

    public enum ServiceState { All = 0, Pending = 1, Running = 2, Completed = 3}
}
