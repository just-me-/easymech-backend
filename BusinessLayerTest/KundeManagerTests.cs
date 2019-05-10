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
    public class KundeManagerTest
    {

        [TestMethod]
        public void AddKundeTest()
        {
            var options = BusinessLayerTestHelper.InitTestDb();
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
            var options = BusinessLayerTestHelper.InitTestDb();
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
        public void GetKundenTest()
        {
            var options = BusinessLayerTestHelper.InitTestDb();
            using (var context = new EMContext(options))
            {
                KundeManager kundeManager = new KundeManager(context);
                var kundenList = kundeManager.GetKunden(false);
                Assert.AreEqual(2, kundenList.Count);
            }
        }

        [TestMethod]
        public void GetKundeByIdTest()
        {
            var options = BusinessLayerTestHelper.InitTestDb();
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
            var options = BusinessLayerTestHelper.InitTestDb();
            using (var context = new EMContext(options))
            {
                KundeManager kundeManager = new KundeManager(context);
                Assert.ThrowsException<InvalidOperationException>(() => kundeManager.GetKundeById(666));
            }
        }
        [TestMethod]
        public void UpdateKundeTest()
        {
            var options = BusinessLayerTestHelper.InitTestDb();
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
            var options = BusinessLayerTestHelper.InitTestDb();
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
            var options = BusinessLayerTestHelper.InitTestDb();
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
            var options = BusinessLayerTestHelper.InitTestDb();
            using (var context = new EMContext(options))
            {
                KundeManager kundeManager = new KundeManager(context);
                Kunde f = new Kunde
                {
                    Id = 1,
                    Firma = "duko Stapler"
                };
                var resultList = kundeManager.GetSearchResult(f);
                Assert.AreEqual(1, resultList.First().Id);
            }
        }
    }
}
