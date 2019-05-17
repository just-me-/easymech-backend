using System;
using EasyMechBackend.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using static EasyMechBackend.Common.EnumHelper;

namespace EasyMechBackend.Common
{
    public class ServiceSearchDto
    {
        public DateTime? Von { get; set; }
        public DateTime? Bis { get; set; }

        private long? _maschinenId;
        public long? MaschinenId
        {
            get => _maschinenId;
            set => _maschinenId = (value == 0) ? null : value;
        }

        private long? _maschinentypId;
        public long? MaschinentypId
        {
            get => _maschinentypId;
            set => _maschinentypId = (value == 0) ? null : value;
        }

        private long? _kundenId;
        public long? KundenId
        {
            get => _kundenId;
            set => _kundenId = (value == 0) ? null : value;
        }
        public ServiceState? Status { get; set; }
    }
}
