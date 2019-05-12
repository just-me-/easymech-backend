using System;

namespace EasyMechBackend.Common.Exceptions
{
    public class ReservationException : Exception
    {
        public ReservationException(string msg) : base(msg) { }
    }
}
