using EasyMechBackend.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UtilTest
{
    [TestClass]
    public class Clip128
    {
        [TestMethod]
        public void TestShort()
        {
            string s = "bla";
            string r = s.ClipTo128Chars();
            Assert.AreEqual(s,r);
        }

        [TestMethod]
        public void TestLong()
        {
            string s = "0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789";
            //                    10        20        30          40       50        60            70   80         90        100      110       120
            string e = "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567";
            string r = s.ClipTo128Chars();
            Assert.AreEqual(e, r);
        }

        [TestMethod]
        public void TestEmpty()
        {
            string s = "";
            string r = s.ClipTo128Chars();
            Assert.AreEqual(s, r);
        }
        [TestMethod]
        public void TestNull()
        {
            string s = null;
            string r = s.ClipTo128Chars();
            Assert.AreEqual(s, r);
        }
    }
}
