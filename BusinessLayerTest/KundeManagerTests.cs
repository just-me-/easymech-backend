using EasyMechBackend.BusinessLayer;
using EasyMechBackend.DataAccessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System;
using EasyMechBackend.Common.Exceptions;
using EasyMechBackend.DataAccessLayer.Entities;

namespace BusinessLayerTest
{
    [TestClass]
    public class KundeManagerTest : ManagerBaseTests
    {

        [TestMethod]
        public void AddKundeTest()
        {
            using (var context = new EMContext(options))
            {
                int id = 3;
                Kunde k = new Kunde
                {
                    Id = id,
                    Firma = "Firma 3",
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
                Assert.AreEqual("Firma 3", addedKunde.Firma);
            }
        }

        [TestMethod]
        public void AddKundeDuplicateTest()
        {
            using (var context = new EMContext(options))
            {
                int id = 3;
                Kunde k = new Kunde
                {
                    Id = id,
                    Firma = "duko Stapler"
                };
                KundeManager kundeManager = new KundeManager(context);
                Assert.ThrowsException<UniquenessException>(() => kundeManager.AddKunde(k));
            }
        }


        [TestMethod]
        public void AddKundeIntoDeleteIntoAddAgainShouldReactivate()
        {
            using (var context = new EMContext(options))
            {

                KundeManager man = new KundeManager(context);
                var original = man.GetKundeById(2);
                man.SetKundeInactive(original);

                man.AddKunde(original);
                var after = man.GetKundeById(2);
                Assert.IsTrue(after.IstAktiv ?? false);
                Assert.AreEqual(2, after.Id);
            }
        }


        [TestMethod]
        public void GetKundenTest()
        {
            var options = BusinessLayerTestHelper.InitTestDb();
            using (var context = new EMContext(options))
            {
                KundeManager kundeManager = new KundeManager(context);
                var kundenList = kundeManager.GetKunden(false);
                Assert.AreEqual(1, kundenList.Count);
            }
        }

        [TestMethod]
        public void GetKundenWithDukoTest()
        {
            var options = BusinessLayerTestHelper.InitTestDb();
            using (var context = new EMContext(options))
            {
                KundeManager kundeManager = new KundeManager(context);
                var kundenList = kundeManager.GetKunden(true);
                Assert.AreEqual(2, kundenList.Count);
            }
        }

        [TestMethod]
        public void GetKundeByIdTest()
        {
            using (var context = new EMContext(options))
            {
                KundeManager kundeManager = new KundeManager(context);
                var kunde = kundeManager.GetKundeById(1);
                Assert.AreEqual("duko Stapler", kunde.Firma);
            }
        }
        [TestMethod]
        public void GetKundeByNonexistantIdTest()
        {
            using (var context = new EMContext(options))
            {
                KundeManager kundeManager = new KundeManager(context);
                Assert.ThrowsException<InvalidOperationException>(() => kundeManager.GetKundeById(666));
            }
        }
        [TestMethod]
        public void UpdateKundeTest()
        {
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
            using (var context = new EMContext(options))
            {
                KundeManager kundeManager = new KundeManager(context);
                var originalTyp = kundeManager.GetKundeById(1);
                kundeManager.DeleteKunde(originalTyp);
                Assert.AreEqual(1, context.Kunden.Count());
            }
        }

        [TestMethod]
        public void GetSearchResultKundeTest()
        {
            using (var context = new EMContext(options))
            {
                KundeManager kundeManager = new KundeManager(context);
                Kunde f = new Kunde
                {
                    Firma = "ERSTER"
                };
                var resultList = kundeManager.GetSearchResult(f);
                Assert.AreEqual(2, resultList.First().Id);
            }
        }
    }
}
