using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using Microsoft.EntityFrameworkCore;
using EasyMechBackend.DataAccessLayer;
using EasyMechBackend.BusinessLayer;
using System.Linq;

namespace BusinessLayerTest
{
    [TestClass]
    public class MaschinentypManagerTests
    {
        private DbContextOptions<EMContext> ResetDBwithMaschinentypHelper()
        {
            var options = new DbContextOptionsBuilder<EMContext>()
                            .UseInMemoryDatabase(databaseName: "MaschinentypTestDB")
                            .Options;

            using (var context = new EMContext(options))
            {
                MaschinentypManager maschinentypManager = new MaschinentypManager(context);
                foreach (Maschinentyp t in maschinentypManager.GetMaschinentypen())
                {
                    context.Remove(t);
                }
                context.SaveChanges();
                Maschinentyp f = new Maschinentyp
                {
                    Id = 1,
                    Fabrikat = "Tester grande",
                    Nutzlast = 2000
                };
                f.Validate();
                context.Add(f);
                context.SaveChanges();
            }

            return options;
        }
        [TestMethod]
        public void AddMaschinentypTest()
        {
            var options = ResetDBwithMaschinentypHelper();
            using (var context = new EMContext(options))
            {
                int id = 2;
                Maschinentyp f = new Maschinentyp
                {
                    Id = id,
                    Fabrikat = "Tester grande",
                    Nutzlast = 2000
                };
                MaschinentypManager maschinentypManager = new MaschinentypManager(context);
                maschinentypManager.AddMaschinentyp(f);
                var addedMaschinentyp = context.Maschinentypen.Single(maschinentyp => maschinentyp.Id == id);
                Assert.AreEqual("Tester grande", addedMaschinentyp.Fabrikat);
            }
        } 
        [TestMethod]
        public void GetMaschinentypenTest()
        {
            var options = ResetDBwithMaschinentypHelper();
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
            var options = ResetDBwithMaschinentypHelper();
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
            var options = ResetDBwithMaschinentypHelper();
            using (var context = new EMContext(options))
            {
                MaschinentypManager maschinentypManager = new MaschinentypManager(context);
                Assert.ThrowsException<InvalidOperationException>(() => maschinentypManager.GetMaschinentypById(666));
            }
        }
        [TestMethod]
        public void UpdateMaschinentypTest()
        {
            var options = ResetDBwithMaschinentypHelper();
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
            var options = ResetDBwithMaschinentypHelper();
            using (var context = new EMContext(options))
            {
                MaschinentypManager maschinentypManager = new MaschinentypManager(context);
                var originalTyp = maschinentypManager.GetMaschinentypById(1);
                maschinentypManager.DeleteMaschinentyp(originalTyp);
                Assert.IsTrue(!context.Maschinentypen.Any());
            }
        }

        [TestMethod]
        public void GetSearchResultMaschinentypTest()
        {
            var options = ResetDBwithMaschinentypHelper();
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
