using EasyMechBackend.BusinessLayer;
using EasyMechBackend.DataAccessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace BusinessLayerTest
{
    [TestClass]
    public class ManagerBaseTests
    {
        // placeholder
    }

    [TestClass]
    public class KundeManagerTest
    {

        DbContextOptions<EMContext> InitDBwithKundeHelper()
        {
            var options = new DbContextOptionsBuilder<EMContext>()
                            .UseInMemoryDatabase(databaseName: "getKundenTestDB")
                            .Options;

            using (var context = new EMContext(options))
            {
                Kunde k = new Kunde
                {
                    Id = 1,
                    Firma = "Test AG",
                    IsActive = true
                };
                k.Validate();
                context.Add(k);
                context.SaveChanges();
            }

            return options;
        }


        [TestMethod]
        public void AddKundeTest()
        {
            var options = InitDBwithKundeHelper();

            using (var context = new EMContext(options))
            {
                Assert.AreEqual(1, context.Kunden.Count());
                Assert.AreEqual("Test AG", context.Kunden.Single().Firma);
            }
        }

        [TestMethod]
        public void GetKundeByIdTest() // Get net
        {
            Kunde k = KundeManager.GetKundeById(1);
            Assert.AreEqual(1, k.Id);
        }

        [TestMethod]
        public void GetKundeByNonexistantIdTest() // Geht net
        {
            Assert.ThrowsException<System.InvalidOperationException>(() => KundeManager.GetKundeById(11112));
        }

        [TestMethod]
        public void GetKundenTest() // Geht net
        {
            List<Kunde> kundenliste = KundeManager.GetKunden();
            Assert.IsTrue(kundenliste.Any());
        }

        [TestMethod]
        public void UpdateKundeTest() // Geht net
        {
            Kunde originalKunde = KundeManager.GetKundeById(1);
            originalKunde.Firma = "Updated AG";
            KundeManager.UpdateKunde(originalKunde);
            Kunde updatedKunde = KundeManager.GetKundeById(1);
            Assert.AreEqual("Updated AG", updatedKunde.Firma);
        }

        [TestMethod]
        public void SetKundeInactiveTest() // Geht net
        {
            Kunde originalKunde = KundeManager.GetKundeById(1);
            originalKunde.IsActive = true;
            KundeManager.UpdateKunde(originalKunde);
            Kunde kunde = KundeManager.GetKundeById(1);
            KundeManager.SetKundeInactive(kunde);
            Kunde deactivatedKunde = KundeManager.GetKundeById(1);
            Assert.AreEqual(false, deactivatedKunde.IsActive);
        }

        [TestMethod]
        public void DeleteKundeTest() // Geht net
        {
            Kunde k = new Kunde
            {
                Id = 1234567,
                Firma = "Test AG",
                IsActive = true
            };
            KundeManager.AddKunde(k);
            KundeManager.DeleteKunde(k);
            Assert.ThrowsException<System.InvalidOperationException>(() => KundeManager.GetKundeById(1234567));
        }

    }
}
