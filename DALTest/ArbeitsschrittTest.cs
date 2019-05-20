using EasyMechBackend.DataAccessLayer.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DALTest
{
    [TestClass]
    public class ArbeitsschrittTest : DalBase
    {
        [TestMethod]
        public void ValidateSimple()
        {
            Arbeitsschritt arbeitsschritt = new Arbeitsschritt()
            {
                Id = 1,
                Bezeichnung = extraLongText,
                Stundenansatz = 55.0,
                Arbeitsstunden = 1,
                ServiceId = 1
            };

            Arbeitsschritt expected = new Arbeitsschritt()
            {
                Id = 1,
                Bezeichnung = clipped256Text,
                Stundenansatz = 55.0,
                Arbeitsstunden = 1,
                ServiceId = 1
            };

            arbeitsschritt.Validate();

            Assert.IsTrue(HaveSameData(expected, arbeitsschritt));
        }
    }
}
