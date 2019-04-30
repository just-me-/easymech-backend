using EasyMechBackend.DataAccessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace DALTest
{
    [TestClass]
    public class MaschinentypTest : DalBase
    {


        [TestMethod]
        public void ValidateSimple()
        {
            Maschinentyp mt = new Maschinentyp
            {
                Id = 1,
                Fabrikat = text,
                Motortyp = text,
                Nutzlast = 5 
            };

            Maschinentyp expected = new Maschinentyp
            {
                Id = 1,
                Fabrikat = text,
                Motortyp = text,
                Nutzlast = 5
            };

            mt.Validate();

            Assert.IsTrue(HaveSameData(expected, mt));
        }

        [TestMethod]
        public void ValidateTooLongFields()
        {
            Maschinentyp mt = new Maschinentyp
            {
                Id = 1,
                Fabrikat = longText,
                Motortyp = longText,
                Nutzlast = 5
            };

            Maschinentyp expected = new Maschinentyp
            {
                Id = 1,
                Fabrikat = clippedText,
                Motortyp = clippedText,
                Nutzlast = 5
            };

            mt.Validate();

            Assert.IsTrue(HaveSameData(expected, mt));
        }

    }
}
