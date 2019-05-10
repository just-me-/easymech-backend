using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using Microsoft.EntityFrameworkCore;
using EasyMechBackend.DataAccessLayer;
using EasyMechBackend.BusinessLayer;
using System.Linq;
using EasyMechBackend.Common.Exceptions;
using EasyMechBackend.DataAccessLayer.Entities;

namespace BusinessLayerTest
{
    [TestClass]
    public class MaschinentypManagerTests
    {
        public DbContextOptions<EMContext> options;

        [TestInitialize]
        public void TestInitialize()
        {
            options = BusinessLayerTestHelper.InitTestDb();
        }

        [TestMethod]
        public void AddMaschinentypTest()
        {
            using (var context = new EMContext(options))
            {
                int id = 2;
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


        //TODO: Add Maschinentyp, use it as type for a machine
        // Delete MAchinenentyp must throw ForeignKeyExcepion

        //TODO: Add Maschinentyp, use it as type for a machine, set this machine to inactive
        // Delete MAchinenentyp must work, delete the machinentyp as well as the machine


        [TestMethod]
        public void GetMaschinentypenTest()
        {
            using (var context = new EMContext(options))
            {
                MaschinentypManager maschinentypManager = new MaschinentypManager(context);
                var maschinentypenList = maschinentypManager.GetMaschinentypen();
                Assert.AreEqual(1, maschinentypenList.Count);
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
        public void DeleteMaschinentypTest()
        {
            using (var context = new EMContext(options))
            {
                MaschinentypManager maschinentypManager = new MaschinentypManager(context);
                var originalTyp = maschinentypManager.GetMaschinentypById(1);
                maschinentypManager.DeleteMaschinentyp(originalTyp);
                Assert.IsTrue(!context.Maschinentypen.Any());
            }
        }

        [TestMethod]
        public void DeleteMaschinentypWithExistingMachinesTest()
        {
            using (var context = new EMContext(options))
            {
                MaschineManager m_man = new MaschineManager(context);
                MaschinentypManager t_man = new MaschinentypManager(context);

                Maschine m1 = new Maschine
                {
                    Id = 1,
                    MaschinentypId = 1
                };
                m_man.AddMaschine(m1);
                
                var t1 = t_man.GetMaschinentypById(1);
                Assert.ThrowsException<ForeignKeyRestrictionException>(() => t_man.DeleteMaschinentyp(t1));

                m_man.DeleteMaschine(m1);

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
