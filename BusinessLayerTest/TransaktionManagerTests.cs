using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using EasyMechBackend.DataAccessLayer;
using EasyMechBackend.BusinessLayer;
using System.Linq;
using EasyMechBackend.DataAccessLayer.Entities;

namespace BusinessLayerTest
{
    [TestClass]
    public class TransaktionManagerTests : ManagerBaseTests
    {
        
        [TestMethod]
        public void AddTransaktionEinkaufTest()
        {
            using (var context = new EMContext(options))
            {
                int id = 3;
                Transaktion t = new Transaktion
                {
                    Id = id,
                    Preis = 60000,
                    Typ = Transaktion.TransaktionsTyp.Einkauf,
                    Datum = DateTime.Now,
                    MaschinenId = 2,
                    Kunde = context.Kunden.Single(k => k.Id == 2)
                };
                TransaktionManager transaktionManager = new TransaktionManager(context);
                transaktionManager.AddTransaktion(t);
                var addedTransaktion = context.Transaktionen.Single(transaktion => transaktion.Id == id);
                Assert.AreEqual(3, addedTransaktion.Id);
                var maschine = context.Maschinen.Single(m => m.Id == addedTransaktion.MaschinenId);
                var newBesitzer = context.Kunden.Single(kunde => kunde.Id == maschine.BesitzerId);
                Assert.AreEqual(1, newBesitzer.Id);
            }
        }
        [TestMethod]
        public void GetTransaktionenTest()
        {
            using (var context = new EMContext(options))
            {
                TransaktionManager transaktionManager = new TransaktionManager(context);
                var transaktionenListe = transaktionManager.GetTransaktionen();
                Assert.AreEqual(2, transaktionenListe.Count);
            }
        }
        [TestMethod]
        public void GetTransaktionByIdTest()
        {
            using (var context = new EMContext(options))
            {
                TransaktionManager transaktionManager = new TransaktionManager(context);
                var transaktion1 = transaktionManager.GetTransaktionById(1);
                Assert.AreEqual(1, transaktion1.Id);
            }
        }
        [TestMethod]
        public void GetTransaktionByNonexistantIdTest()
        {
            using (var context = new EMContext(options))
            {
                TransaktionManager transaktionManager = new TransaktionManager(context);
                Assert.ThrowsException<InvalidOperationException>(() => transaktionManager.GetTransaktionById(666));
            }
        }
        [TestMethod]
        public void UpdateTransaktionPreisTest()
        {
            using (var context = new EMContext(options))
            {
                TransaktionManager transaktionManager = new TransaktionManager(context);
                var original = transaktionManager.GetTransaktionById(1);
                original.Preis = 66666;
                transaktionManager.UpdateTransaktion(original);
                var updated = transaktionManager.GetTransaktionById(1);
                Assert.AreEqual(66666, updated.Preis);
            }
        }

        [TestMethod]
        public void UpdateTransaktionVerkaeuferTest()
        {
            var options = BusinessLayerTestHelper.InitTestDb();
            using (var context = new EMContext(options))
            {
                TransaktionManager transaktionManager = new TransaktionManager(context);
                var original = transaktionManager.GetTransaktionById(1);
                original.KundenId = 2;
                transaktionManager.UpdateTransaktion(original);
                var updated = transaktionManager.GetTransaktionById(1);
                Assert.AreEqual(2, updated.KundenId);
            }
        }


        [TestMethod]
        public void GetSearchResultTransaktionTest()
        {
            using (var context = new EMContext(options))
            {
                TransaktionManager transaktionManager = new TransaktionManager(context);
                Transaktion m = new Transaktion
                {
                    Id = 1,
                    Preis = 50000
                };
                var resultList = transaktionManager.GetSearchResult(m);
                Assert.AreEqual(1, resultList.First().Id);
            }
        }

    }
}
