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
        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(true);
        }
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
                    Id = 12345,
                    Firma = "Test AG",
                    IsActive = true
                };
                k.Validate();
                try
                {
                    context.Add(k);
                    context.SaveChanges();
                }
                catch (System.ArgumentException e)
                {
                    string error = e.Message;
                }
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
        public void GetKundeByIdTest()
        {
            var options = InitDBwithKundeHelper();
            using (var context = new EMContext(options))
            {
                Kunde k = context.Kunden.SingleOrDefault(kunde => kunde.Id == 12345);
                Assert.AreEqual(12345, k.Id);
            }

        }

        [TestMethod]
        public void GetKundeByNonexistantIdTest()
        {
            var options = InitDBwithKundeHelper();
            using (var context = new EMContext(options))
            {
                Assert.ThrowsException<System.InvalidOperationException>(() => context.Kunden.Single(kunde => kunde.Id == 100000));
                
            }
        }

        [TestMethod]
        public void GetKundenTest()
        {
            var options = InitDBwithKundeHelper();
            using (var context = new EMContext(options))
            {
                List<Kunde> kundenliste = context.Kunden.ToList();
                Assert.IsTrue(kundenliste.Any());
            }
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            var options = InitDBwithKundeHelper();
            using (var context = new EMContext(options))
            {
                Kunde originalKunde = context.Kunden.SingleOrDefault(kunde => kunde.Id == 12345);
                originalKunde.Firma = "Updated AG";
                context.Entry(originalKunde).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                Kunde updatedKunde = context.Kunden.SingleOrDefault(kunde => kunde.Id == 12345);
                Assert.AreEqual("Updated AG", updatedKunde.Firma);
            }
        }

        [TestMethod]
        public void SetKundeInactiveTest()
        {
            var options = InitDBwithKundeHelper();
            using (var context = new EMContext(options))
            {
                Kunde originalKunde = context.Kunden.SingleOrDefault(kunde => kunde.Id == 12345);
                originalKunde.IsActive = false;
                context.Entry(originalKunde).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                Kunde updatedKunde = context.Kunden.SingleOrDefault(kunde => kunde.Id == 12345);
                Assert.AreEqual(false, updatedKunde.IsActive);
            }
        }

        [TestMethod]
        public void DeleteKundeTest()
        {
            var options = InitDBwithKundeHelper();
            using (var context = new EMContext(options))
            {
                Kunde k = context.Kunden.SingleOrDefault(kunde => kunde.Id == 12345);
                context.Remove(k);
                context.SaveChanges();
                Assert.ThrowsException<System.InvalidOperationException>(() => context.Kunden.Single(kunde => kunde.Id == 12345));

            }
        }

        [TestMethod]
        public void GetSearchResultsTest()
        {
            Kunde searchEntity = new Kunde
            {
                Id = 12345,
                Firma = "Test AG",
                IsActive = true
            };
            var options = InitDBwithKundeHelper();
            using (var context = new EMContext(options))
            {

                Assert.IsTrue(true);
            }
        }
    }
}
