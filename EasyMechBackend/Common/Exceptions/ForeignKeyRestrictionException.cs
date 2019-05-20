using System;

namespace EasyMechBackend.Common.Exceptions
{
    public class ForeignKeyRestrictionException : Exception
    {
        public ForeignKeyRestrictionException(string msg) : base(msg)
        {
        }
    }
}
