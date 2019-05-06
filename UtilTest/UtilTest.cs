using EasyMechBackend.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UtilTest
{
    [TestClass]
    public class StringClipper
    {
        [TestMethod]
        public void TestShort()
        {
            string s = "bla";
            string r = s.ClipToNChars(128);
            Assert.AreEqual(s,r);
        }

        [TestMethod]
        public void TestLong()
        {
            string s = "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567XXXXXXXX";
            //                    10        20        30          40       50        60            70   80         90        100      110       120
            string e = "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567";
            string r = s.ClipToNChars(128);
            Assert.AreEqual(e, r);
        }

        [TestMethod]
        public void TestEdgeCase()
        {
            string s = "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567";
            //                    10        20        30          40       50        60            70   80         90        100      110       120
            string e = "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567";
            string r = s.ClipToNChars(128);
            Assert.AreEqual(e, r);
        }

        [TestMethod]
        public void TestEmpty()
        {
            string s = "";
            string r = s.ClipToNChars(128);
            Assert.AreEqual(s, r);
        }
        [TestMethod]
        public void TestNull()
        {
            string s = null;
            string r = s.ClipToNChars(128);
            Assert.AreEqual(s, r);
        }

        [TestMethod]
        public void Test0Input()
        {
            string s = "abc";
            string r = s.ClipToNChars(0);
            string e = "";
            Assert.AreEqual(e, r);
        }
    }


    [TestClass]
    public class HasSearchTerm
    {
        [TestMethod]
        public void TestNegativeSearchTerm()
        {
            string s = "a";
            bool r = s.HasSearchTerm();
            Assert.IsFalse(r);
        }

        [TestMethod]
        public void TestPositiveSearchTermEdgeCase()
        {
            string s = "ch";
            bool r = s.HasSearchTerm();
            Assert.IsTrue(r);
        }

        [TestMethod]
        public void TestEmpty()
        {
            string s = "";
            bool r = s.HasSearchTerm();
            Assert.IsFalse(r);
        }

        [TestMethod]
        public void TestNull()
        {
            string s = null;
            bool r = s.HasSearchTerm();
            Assert.IsFalse(r);
        }
    }


    [TestClass]
    public class ContainsCaseInsensitive
    {
        readonly string baseString = "AbCd123";

        [TestMethod]
        public void TestRegular()
        {
            string s = "bCd";
            Assert.IsTrue(baseString.ContainsCaseInsensitive(s));
        }

        [TestMethod]
        public void TestCaseInsensitivity()
        {
            string s = "BcD";
            Assert.IsTrue(baseString.ContainsCaseInsensitive(s));
        }

        [TestMethod]
        public void TestNumbers()
        {
            string s = "bCd123";
            Assert.IsTrue(baseString.ContainsCaseInsensitive(s));
        }

        [TestMethod]
        public void TestEmpty()
        {
            string s = "";
            Assert.IsTrue(baseString.ContainsCaseInsensitive(s));
            //returns true according to string.Contains() rules which apply here as well
            //this way we dont filter any possible search matches
        }

        [TestMethod]
        public void TestNull()
        {
            string s = null;
            Assert.IsTrue(baseString.ContainsCaseInsensitive(s));
            //for our purpose "true" makes more sense (does not filter results)
        }

        [TestMethod]
        public void TestNegativeResult()
        {
            string s = "abcf";
            Assert.IsFalse(baseString.ContainsCaseInsensitive(s));
        }
    }

}
