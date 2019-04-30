using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.Common.Exceptions
{
    public class ForeignKeyRestrictionException : Exception
    {

        public ForeignKeyRestrictionException(string msg) : base(msg)
        {
        }
    }
}
