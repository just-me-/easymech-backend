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
    public class TransaktionManagerTests
    {
        private DbContextOptions<EMContext> ResetDBwithTransaktionHelper()
        {
            var options = new DbContextOptionsBuilder<EMContext>()
                            .UseInMemoryDatabase(databaseName: "TransaktionTestDB")
                            .Options;

            using (var context = new EMContext(options))
            {
                TransaktionManager transaktionManager = new TransaktionManager(context);
                KundeManager kundeManager = new KundeManager(context);
                MaschineManager maschineManager = new MaschineManager(context);
                foreach (Transaktion t in transaktionManager.GetTransaktionen())
                {
                    context.Remove(t);
                }
                foreach (Kunde k in kundeManager.GetKunden(true))
                {
                    context.Remove(k);
                }
                foreach (Maschine m in maschineManager.GetMaschinen(true))
                {
                    context.Remove(m);
                }
                context.SaveChanges();
                Kunde k1 = new Kunde
                {
                    Id = 1,
                    Firma = "Firma",
                    Vorname = "Ben",
                    Nachname = "S",
                    PLZ = "9010",
                    Ort = "St.Gallen",
                    Email = "test@b.ch",
                    Telefon = "+41 11 133 45 48",
                    Notiz = "test Notiz"
                };
                k1.Validate();
                Kunde k2 = new Kunde
                {
                    Id = 2,
                    Firma = "Firma2",
                    Vorname = "Ben2",
                    Nachname = "S2",
                    PLZ = "9010",
                    Ort = "St.Gallen",
                    Email = "test2@b.ch",
                    Telefon = "+41 11 133 45 48",
                    Notiz = "test Notiz2"
                };
                k2.Validate();

                Maschine m2 = new Maschine
                {
                    Id = 1,
                    Seriennummer = "123xyz",
                    Jahrgang = 1999,
                    IstAktiv = true,
                    Besitzer = k1
                };
                m2.Validate();

                Transaktion startTransaktionEinkauf = new Transaktion
                {
                    Id = 1,
                    Preis = 50000,
                    Typ = Transaktion.TransaktionsTyp.Einkauf,
                    Datum = DateTime.Now,
                    MaschinenId = 1,
                    KundenId = 1
                };

                Transaktion startTransaktionVerkauf = new Transaktion
                {
                    Id = 2,
                    Preis = 45000,
                    Typ = Transaktion.TransaktionsTyp.Verkauf,
                    Datum = DateTime.Now,
                    MaschinenId = 2,
                    KundenId = 1
                };
                context.Add(k1);
                context.Add(k2);
                context.Add(m2);
                context.Add(startTransaktionEinkauf);
                context.Add(startTransaktionVerkauf);
                context.SaveChanges();
            }
            return options;
        }
        [TestMethod]
        public void AddTransaktionEinkaufTest()
        {
            var options = ResetDBwithTransaktionHelper();
            using (var context = new EMContext(options))
            {
                int id = 3;
                Transaktion m = new Transaktion
                {
                    Id = id,
                    Preis = 60000,
                    Typ = Transaktion.TransaktionsTyp.Einkauf,
                    Datum = DateTime.Now,
                    MaschinenId = 1,
                    KundenId = 1
                };
                TransaktionManager transaktionManager = new TransaktionManager(context);
                transaktionManager.AddTransaktion(m);
                var addedTransaktion = context.Transaktionen.Single(transaktion => transaktion.Id == id);
                Assert.AreEqual(3, addedTransaktion.Id);
            }
        }
        [TestMethod]
        public void GetTransaktionenTest()
        {
            var options = ResetDBwithTransaktionHelper();
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
            var options = ResetDBwithTransaktionHelper();
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
            var options = ResetDBwithTransaktionHelper();
            using (var context = new EMContext(options))
            {
                TransaktionManager transaktionManager = new TransaktionManager(context);
                Assert.ThrowsException<InvalidOperationException>(() => transaktionManager.GetTransaktionById(666));
            }
        }
        [TestMethod]
        public void UpdateTransaktionPreisTest()
        {
            var options = ResetDBwithTransaktionHelper();
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
            var options = ResetDBwithTransaktionHelper();
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
            var options = ResetDBwithTransaktionHelper();
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
