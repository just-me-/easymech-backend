using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.Util
{
    public static class Helpers
    {
        public static string ClipTo128Chars(this string s)
        {
            if (s == null) {
              return null;
            }
            if (s.Length > 128)
            {
                s = s.Remove(128);
            }
            return s;

        }

        public static bool HasSearchTerm(this string s)
        {
            return s?.Length >= 3;
        }
    }
}
