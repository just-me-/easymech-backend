
namespace EasyMechBackend.Util
{
    public static class Helpers
    {
        public static string ClipToNChars(this string s, int length)
        {
            if (s == null) {
              return null;
            }
            if (s.Length > length)
            {
                s = s.Remove(length);
            }
            return s;

        }

        public static bool HasSearchTerm(this string s)
        {
            return s?.Length >= 2;
        }

        public static bool ContainsCaseInsensitive(this string s1, string s2)
        {
            if (s2 == null) return true;
            string a1 = s1.ToLower();
            string a2 = s2.ToLower();
            return a1.Contains(a2);
        }
    }
}
