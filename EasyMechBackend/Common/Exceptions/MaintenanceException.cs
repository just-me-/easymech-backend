using System;

namespace EasyMechBackend.Common.Exceptions
{
    public class MaintenanceException : Exception
    {
        public MaintenanceException(string msg) : base(msg) { }
    }
}
