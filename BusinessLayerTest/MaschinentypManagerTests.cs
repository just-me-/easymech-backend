using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyMechBackend.DataAccessLayer;
using EasyMechBackend.BusinessLayer;
using System.Linq;
using EasyMechBackend.Common.Exceptions;
using EasyMechBackend.DataAccessLayer.Entities;

namespace BusinessLayerTest
{
    [TestClass]
    public class MaschinentypManagerTests : ManagerBaseTests
    {
        [TestMethod]
        public void AddMaschinentypTest()
        {
            using (var context = new EMContext(options))
            {
                int id = 3;
                Maschinentyp f = new Maschinentyp
                {
                    Id = id,
                    Fabrikat = "Tester grande 2",
                    Nutzlast = 2000
                };
                MaschinentypManager maschinentypManager = new MaschinentypManager(context);
                maschinentypManager.AddMaschinentyp(f);
                var addedMaschinentyp = context.Maschinentypen.Single(maschinentyp => maschinentyp.Id == id);
                Assert.AreEqual("Tester grande 2", addedMaschinentyp.Fabrikat);
            }
        }

        [TestMethod]
        public void AddDuplicateTypTest()
        {
            using (var context = new EMContext(options))
            {
                Maschinentyp t = new Maschinentyp
                {
                    Id = 2,
                    Fabrikat = "Tester grande"
                };
                MaschinentypManager man = new MaschinentypManager(context);
                Assert.ThrowsException<UniquenessException>(() => man.AddMaschinentyp(t));
            }
        }

        [TestMethod]
        public void GetMaschinentypenTest()
        {
            using (var context = new EMContext(options))
            {
                MaschinentypManager maschinentypManager = new MaschinentypManager(context);
                var maschinentypenList = maschinentypManager.GetMaschinentypen();
                Assert.AreEqual(2, maschinentypenList.Count);
            }
        }

        [TestMethod]
        public void GetMaschinentypByIdTest()
        {
            using (var context = new EMContext(options))
            {
                MaschinentypManager maschinentypManager = new MaschinentypManager(context);
                var maschinentyp1 = maschinentypManager.GetMaschinentypById(1);
                Assert.AreEqual("Tester grande", maschinentyp1.Fabrikat);
            }
        }

        [TestMethod]
        public void GetMaschinentypByNonexistantIdTest()
        {
            using (var context = new EMContext(options))
            {
                MaschinentypManager maschinentypManager = new MaschinentypManager(context);
                Assert.ThrowsException<InvalidOperationException>(() => maschinentypManager.GetMaschinentypById(666));
            }
        }

        [TestMethod]
        public void UpdateMaschinentypTest()
        {
            using (var context = new EMContext(options))
            {
                MaschinentypManager maschinentypManager = new MaschinentypManager(context);
                var originalTyp = maschinentypManager.GetMaschinentypById(1);
                originalTyp.Fabrikat = "Updated Fabrikat";
                maschinentypManager.UpdateMaschinentyp(originalTyp);
                var updatedTyp = maschinentypManager.GetMaschinentypById(1);
                Assert.AreEqual("Updated Fabrikat", updatedTyp.Fabrikat);
            }
        }
        
        [TestMethod]
        public void DeleteMaschinentypWithExistingMachinesTest()
        {
            using (var context = new EMContext(options))
            {
                MaschinentypManager t_man = new MaschinentypManager(context);
                var t1 = t_man.GetMaschinentypById(1);
                Assert.ThrowsException<ForeignKeyRestrictionException>(() => t_man.DeleteMaschinentyp(t1));
            }
        }

        [TestMethod]
        public void DeleteMaschinentypWithoutExistingMachinesTest()
        {
            using (var context = new EMContext(options))
            {
                MaschinentypManager t_man = new MaschinentypManager(context);
                var t1 = t_man.GetMaschinentypById(2);
                t_man.DeleteMaschinentyp(t1);
                Assert.AreEqual(1, context.Maschinentypen.Count());
            }
        }

        [TestMethod]
        public void GetSearchResultMaschinentypTest()
        {
            using (var context = new EMContext(options))
            {
                MaschinentypManager maschinentypManager = new MaschinentypManager(context);
                Maschinentyp f = new Maschinentyp
                {
                    Id = 0,
                    Fabrikat = "Tester grande",
                    Nutzlast = 2000
                };
                var resultList = maschinentypManager.GetSearchResult(f);
                Assert.AreEqual(1, resultList.First().Id);
            }
        }
    }
}
