using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyMechBackend.DataAccessLayer;
using EasyMechBackend.BusinessLayer;
using System.Linq;
using EasyMechBackend.DataAccessLayer.Entities;
using EasyMechBackend.Common.DataTransferObject;
using static EasyMechBackend.Common.EnumHelper;

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
        public void AddTransaktionVerkaufTest()
        {
            using (var context = new EMContext(options))
            {
                int id = 3;
                Transaktion t = new Transaktion
                {
                    Id = id,
                    Preis = 40000,
                    Typ = Transaktion.TransaktionsTyp.Verkauf,
                    Datum = DateTime.Now,
                    MaschinenId = 1,
                    Kunde = context.Kunden.Single(k => k.Id == 2)
                };
                TransaktionManager transaktionManager = new TransaktionManager(context);
                transaktionManager.AddTransaktion(t);
                var addedTransaktion = context.Transaktionen.Single(transaktion => transaktion.Id == id);
                Assert.AreEqual(3, addedTransaktion.Id);
                var maschine = context.Maschinen.Single(m => m.Id == addedTransaktion.MaschinenId);
                var newBesitzer = context.Kunden.Single(kunde => kunde.Id == maschine.BesitzerId);
                Assert.AreEqual(2, newBesitzer.Id);
            }
        }

        [TestMethod]
        public void AddTransaktionForNonexistantMaschineTest()
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
                    MaschinenId = 666,
                    Kunde = context.Kunden.Single(k => k.Id == 2)
                };
                TransaktionManager transaktionManager = new TransaktionManager(context);
                Assert.ThrowsException<InvalidOperationException>(() => transaktionManager.AddTransaktion(t));
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
    }

    [TestClass]
    public class ReservationsManagerSearchTests : ManagerBaseTests
    {
        [TestMethod]
        public void TestCustomerSearch()
        {
            using (var context = new EMContext(options))
            {
                var man = new TransaktionManager(context);
                var searchEntity = new ServiceSearchDto
                {
                    KundenId = 1
                };
                var result = man.GetServiceSearchResult(searchEntity);

                Assert.AreEqual(2, result.Count);
            }
        }

        [TestMethod]
        public void TestMachineSearch()
        {
            using (var context = new EMContext(options))
            {
                var man = new TransaktionManager(context);
                var searchEntity = new ServiceSearchDto
                {
                    MaschinenId = 2
                };
                var result = man.GetServiceSearchResult(searchEntity);

                Assert.AreEqual(2, result.Single().Id);
            }
        }

        [TestMethod]
        public void TestMachinetypeSearchWithMatches()
        {
            using (var context = new EMContext(options))
            {
                var man = new TransaktionManager(context);
                var searchEntity = new ServiceSearchDto
                {
                    MaschinentypId = 1
                };
                var result = man.GetServiceSearchResult(searchEntity);

                Assert.AreEqual(2, result.Count);
            }
        }

        [TestMethod]
        public void TestMachinetypeSearchWithoutMatches()
        {
            using (var context = new EMContext(options))
            {
                var man = new TransaktionManager(context);
                var searchEntity = new ServiceSearchDto
                {
                    MaschinentypId = 2
                };
                var result = man.GetServiceSearchResult(searchEntity);

                Assert.IsFalse(result.Any());
            }
        }

        [TestMethod]
        public void TestFromDateWithPartialMatches()
        {
            using (var context = new EMContext(options))
            {
                var man = new TransaktionManager(context);
                var searchEntity = new ServiceSearchDto
                {
                    Von = new DateTime(2019, 05, 16)
                };
                var result = man.GetServiceSearchResult(searchEntity);

                Assert.AreEqual(2, result.Single().Id);
            }
        }

        [TestMethod]
        public void TestToDateWithPartialMatchches()
        {
            using (var context = new EMContext(options))
            {
                var man = new TransaktionManager(context);
                var searchEntity = new ServiceSearchDto
                {
                    Bis = new DateTime(2019, 05, 15)
                };
                var result = man.GetServiceSearchResult(searchEntity);

                Assert.AreEqual(1, result.Single().Id);
            }
        }

        [TestMethod]
        public void EnsureStateFieldIsPointless()
        {
            using (var context = new EMContext(options))
            {
                var man = new TransaktionManager(context);
                var searchEntity1 = new ServiceSearchDto
                {
                    Bis = new DateTime(2019, 05, 15),
                    Status = ServiceState.All
                };
                var searchEntity2 = new ServiceSearchDto
                {
                    Bis = new DateTime(2019, 05, 15),
                    Status = ServiceState.Completed
                };
                var result1 = man.GetServiceSearchResult(searchEntity1);
                var result2 = man.GetServiceSearchResult(searchEntity2);

                Assert.AreEqual(result1.Count, result2.Count);
            }
        }

        [TestMethod]
        public void TestItAllTogether()
        {
            using (var context = new EMContext(options))
            {
                var man = new TransaktionManager(context);
                var searchEntity1 = new ServiceSearchDto
                {
                    MaschinenId = 1,
                    KundenId = 1,
                    MaschinentypId = 1,
                    Von = new DateTime(2012, 01, 01),
                    Bis = new DateTime(2019, 05, 15),
                    Status = ServiceState.All
                };

                var result1 = man.GetServiceSearchResult(searchEntity1);

                Assert.AreEqual(1, result1.Single().Id);
            }
        }
    }
}
