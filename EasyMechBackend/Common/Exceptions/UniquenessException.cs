using System;

namespace EasyMechBackend.Common.Exceptions
{
    public class UniquenessException : Exception
    {
        public UniquenessException(string msg) : base(msg) { }
    }
}
