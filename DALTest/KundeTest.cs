using EasyMechBackend.DataAccessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace DALTest
{
    [TestClass]
    public class KundeTest : DalBase
    {


        [TestMethod]
        public void ValidateSimple()
        {
            Kunde cust = new Kunde
            {
                Id = 1,
                Firma = text,
                Vorname = text,
                Nachname = text,
                Adresse = text,
                PLZ = text,
                Ort = text,
                Email = text,
                Telefon = text,
                Notiz = text,
                IstAktiv = true
            };
            Kunde expected = new Kunde
            {
                Id = 1,
                Firma = text,
                Vorname = text,
                Nachname = text,
                Adresse = text,
                PLZ = text,
                Ort = text,
                Email = text,
                Telefon = text,
                Notiz = text,
                IstAktiv = true
            };

            cust.Validate();

            Assert.IsTrue(HaveSameData(expected, cust));
        }




        [TestMethod]
        public void ValidateMissingFields()
        {
            Kunde cust = new Kunde
            {
                Id = 1
            };
            Kunde expected = new Kunde
            {
                Id = 1,
                Firma = "",
                IstAktiv = true
            };

            cust.Validate();

            Assert.IsTrue(HaveSameData(expected, cust));
        }

        [TestMethod]
        public void ValidateTooLongFields()
        {
            
            Kunde cust = new Kunde
            {
                Id = 1,
                Firma = longText,
                Vorname = longText,
                Nachname = longText,
                Adresse = longText,
                PLZ = longText,
                Ort = longText,
                Email = longText,
                Telefon = longText,
                Notiz = longText,
                IstAktiv = true
            };

            Kunde expected = new Kunde
            {
                Id = 1,
                Firma = clippedText,
                Vorname = clippedText,
                Nachname = clippedText,
                Adresse = clippedText,
                PLZ = clippedText,
                Ort = clippedText,
                Email = clippedText,
                Telefon = clippedText,
                Notiz = longText, // unlmited
                IstAktiv = true
            };

            cust.Validate();

            Assert.IsTrue(HaveSameData(expected, cust));
        }
    }
}
