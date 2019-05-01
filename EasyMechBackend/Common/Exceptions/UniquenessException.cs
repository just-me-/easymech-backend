using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.Common.Exceptions
{
    public class UniquenessException : Exception
    {
        public UniquenessException(string msg) : base(msg) { }
    }
}
