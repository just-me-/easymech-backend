using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.Common
{
    public static class EnumHelper
    {
        public enum ServiceState { All = 0, Pending = 1, Running = 2, Completed = 3 }
    }
}
