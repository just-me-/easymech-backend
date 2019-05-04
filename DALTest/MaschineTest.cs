using EasyMechBackend.DataAccessLayer;
using EasyMechBackend.DataAccessLayer.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DALTest
{
    [TestClass]
    public class MaschineTest : DalBase
    {

        [TestMethod]
        public void ValidateSimple()
        {
            Maschine machine = new Maschine
            {
                Id = 1,
                Seriennummer = text,
                Mastnummer = text,
                Motorennummer = text,
                Betriebsdauer = 5,
                Jahrgang = 1986,
                Notiz = longText,
                BesitzerId = 1,
                MaschinentypId = 1,
                IstAktiv = true
            };

            Maschine expected = new Maschine
            {
                Id = 1,
                Seriennummer = text,
                Mastnummer = text,
                Motorennummer = text,
                Betriebsdauer = 5,
                Jahrgang = 1986,
                Notiz = longText,
                BesitzerId = 1,
                MaschinentypId = 1,
                IstAktiv = true
            };

            machine.Validate();

            Assert.IsTrue(HaveSameData(expected, machine));
        }




        [TestMethod]
        public void ValidateMissingFields()
        {
            Maschine machine = new Maschine
            {
                Id = 1,
                BesitzerId = 1,
                MaschinentypId = 1
            };

            Maschine expected = new Maschine
            {
                Id = 1,
                BesitzerId = 1,
                MaschinentypId = 1,
                IstAktiv = true
            };

            machine.Validate();

            Assert.IsTrue(HaveSameData(expected, machine));
        }

        [TestMethod]
        public void ValidateTooLongFields()
        {
            Maschine machine = new Maschine
            {
                Id = 1,
                Seriennummer = longText,
                Mastnummer = longText,
                Motorennummer = longText,
                Notiz = longText,
                BesitzerId = 1,
                MaschinentypId = 1,
                IstAktiv = true
            };

            Maschine expected = new Maschine
            {
                Id = 1,
                Seriennummer = clippedText,
                Mastnummer = clippedText,
                Motorennummer = clippedText,
                Notiz = longText,
                BesitzerId = 1,
                MaschinentypId = 1,
                IstAktiv = true
            };

            machine.Validate();

            Assert.IsTrue(HaveSameData(expected, machine));
        }
    }
}
