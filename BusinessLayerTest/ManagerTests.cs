using EasyMechBackend.BusinessLayer;
using EasyMechBackend.DataAccessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System;
using EasyMechBackend.DataAccessLayer.Entities;

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

        private DbContextOptions<EMContext> ResetDBwithKundeHelper()
        {
            var options = new DbContextOptionsBuilder<EMContext>()
                            .UseInMemoryDatabase(databaseName: "KundeTestDB")
                            .Options;

            using (var context = new EMContext(options))
            {
                KundeManager kundeManager = new KundeManager(context);
                foreach (Kunde k in kundeManager.GetKunden(false))
                {
                    context.Remove(k);
                }
                context.SaveChanges();
                Kunde k1 = new Kunde
                {
                    Id = 1,
                    Firma = "Firma",
                    Vorname = "Tom",
                    Nachname = "K",
                    PLZ = "7000",
                    Ort = "Chur",
                    Email = "t@b.ch",
                    Telefon = "+41 81 123 45 68",
                    Notiz = "Zahlt immer pünktlich, ist ganz nett.\nDarf weider mal eine Maschine mieten"
                };
                k1.Validate();
                context.Add(k1);
                context.SaveChanges();
            }

            return options;
        }
        [TestMethod]
        public void AddKundeTest()
        {
            var options = ResetDBwithKundeHelper();
            using (var context = new EMContext(options))
            {
                int id = 2;
                Kunde k = new Kunde
                {
                    Id = id,
                    Firma = "Firma 2",
                    Vorname = "Tom",
                    Nachname = "K",
                    PLZ = "7000",
                    Ort = "Chur",
                    Email = "t@b.ch",
                    Telefon = "+41 81 123 45 68"
                };
                KundeManager kundeManager = new KundeManager(context);
                kundeManager.AddKunde(k);
                var addedKunde = context.Kunden.Single(kunde => kunde.Id == id);
                Assert.AreEqual("Firma 2", addedKunde.Firma);
            }
        }
        [TestMethod]
        public void GetKundenTest()
        {
            var options = ResetDBwithKundeHelper();
            using (var context = new EMContext(options))
            {
                KundeManager kundeManager = new KundeManager(context);
                var kundenList = kundeManager.GetKunden(false);
                Assert.AreEqual(1, kundenList.Count);
            }
        }

        [TestMethod]
        public void GetKundeByIdTest()
        {
            var options = ResetDBwithKundeHelper();
            using (var context = new EMContext(options))
            {
                KundeManager kundeManager = new KundeManager(context);
                var kunde = kundeManager.GetKundeById(1);
                Assert.AreEqual("Firma", kunde.Firma);
            }
        }
        [TestMethod]
        public void GetKundeByNonexistantIdTest()
        {
            var options = ResetDBwithKundeHelper();
            using (var context = new EMContext(options))
            {
                KundeManager kundeManager = new KundeManager(context);
                Assert.ThrowsException<InvalidOperationException>(() => kundeManager.GetKundeById(666));
            }
        }
        [TestMethod]
        public void UpdateKundeTest()
        {
            var options = ResetDBwithKundeHelper();
            using (var context = new EMContext(options))
            {
                KundeManager kundeManager = new KundeManager(context);
                var originalTyp = kundeManager.GetKundeById(1);
                originalTyp.Firma = "Updated Firma";
                kundeManager.UpdateKunde(originalTyp);
                var updatedTyp = kundeManager.GetKundeById(1);
                Assert.AreEqual("Updated Firma", updatedTyp.Firma);
            }
        }

        [TestMethod]
        public void SetInactiveTest()
        {
            var options = ResetDBwithKundeHelper();
            using (var context = new EMContext(options))
            {
                KundeManager man = new KundeManager(context);
                var original = man.GetKundeById(1);
                man.SetKundeInactive(original);
                var updated = man.GetKundeById(1);
                Assert.IsFalse(updated.IstAktiv ?? true);
            }
        }

        [TestMethod]
        public void DeleteKundeTest()
        {
            var options = ResetDBwithKundeHelper();
            using (var context = new EMContext(options))
            {
                KundeManager kundeManager = new KundeManager(context);
                var originalTyp = kundeManager.GetKundeById(1);
                kundeManager.DeleteKunde(originalTyp);
                Assert.IsTrue(!context.Kunden.Any());
            }
        }

        [TestMethod]
        public void GetSearchResultKundeTest()
        {
            var options = ResetDBwithKundeHelper();
            using (var context = new EMContext(options))
            {
                KundeManager kundeManager = new KundeManager(context);
                Kunde f = new Kunde
                {
                    Id = 0,
                    Firma = "Firma"
                };
                var resultList = kundeManager.GetSearchResult(f);
                Assert.AreEqual(1, resultList.First().Id);
            }
        }
    }
}
