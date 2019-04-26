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
    public class MaschineManagerTests
    {
        private DbContextOptions<EMContext> ResetDBwithMaschineHelper()
        {
            var options = new DbContextOptionsBuilder<EMContext>()
                            .UseInMemoryDatabase(databaseName: "MaschineTestDB")
                            .Options;

            using (var context = new EMContext(options))
            {
                MaschineManager maschineManager = new MaschineManager(context);
                foreach (Maschine m in maschineManager.GetMaschinen())
                {
                    context.Remove(m);
                }
                context.SaveChanges();
                Maschine m2 = new Maschine
                {
                    Id = 1,
                    Seriennummer = "123xyz",
                    Jahrgang = 1999,
                    IstAktiv = true
                };
                m2.Validate();
                context.Add(m2);
                context.SaveChanges();
            }
            return options;
        }
        [TestMethod]
        public void AddMaschineTest()
        {
            var options = ResetDBwithMaschineHelper();
            using (var context = new EMContext(options))
            {
                int id = 2;
                Maschine m = new Maschine
                {
                    Id = id,
                    Seriennummer = "123xyz",
                    Jahrgang = 1999,
                    IstAktiv = true
                };
                MaschineManager maschineManager = new MaschineManager(context);
                maschineManager.AddMaschine(m);
                var addedMaschine = context.Maschinen.Single(maschine => maschine.Id == id);
                Assert.AreEqual("123xyz", addedMaschine.Seriennummer);
            }
        } 
        [TestMethod]
        public void GetMaschinenTest()
        {
            var options = ResetDBwithMaschineHelper();
            using (var context = new EMContext(options))
            {
                MaschineManager maschineManager = new MaschineManager(context);
                var maschinenList = maschineManager.GetMaschinen();
                Assert.AreEqual(1, maschinenList.Count);
            }
        }
        [TestMethod]
        public void GetMaschineByIdTest()
        {
            var options = ResetDBwithMaschineHelper();
            using (var context = new EMContext(options))
            {
                MaschineManager maschineManager = new MaschineManager(context);
                var maschine1 = maschineManager.GetMaschineById(1);
                Assert.AreEqual("123xyz", maschine1.Seriennummer);
            }
        }
        [TestMethod]
        public void GetMaschineByNonexistantIdTest()
        {
            var options = ResetDBwithMaschineHelper();
            using (var context = new EMContext(options))
            {
                MaschineManager maschineManager = new MaschineManager(context);
                Assert.ThrowsException<InvalidOperationException>(() => maschineManager.GetMaschineById(666));
            }
        }
        [TestMethod]
        public void UpdateMaschineTest()
        {
            var options = ResetDBwithMaschineHelper();
            using (var context = new EMContext(options))
            {
                MaschineManager maschineManager = new MaschineManager(context);
                var original = maschineManager.GetMaschineById(1);
                original.Seriennummer = "updated Serialnr";
                maschineManager.UpdateMaschine(original);
                var updated = maschineManager.GetMaschineById(1);
                Assert.AreEqual("updated Serialnr", updated.Seriennummer);
            }
        }

        [TestMethod]
        public void DeleteMaschineTest()
        {
            var options = ResetDBwithMaschineHelper();
            using (var context = new EMContext(options))
            {
                MaschineManager maschineManager = new MaschineManager(context);
                var maschine = maschineManager.GetMaschineById(1);
                maschineManager.DeleteMaschine(maschine);
                Assert.IsTrue(!context.Maschinen.Any());
            }
        }

        [TestMethod]
        public void GetSearchResultMaschineTest()
        {
            var options = ResetDBwithMaschineHelper();
            using (var context = new EMContext(options))
            {
                MaschineManager maschineManager = new MaschineManager(context);
                Maschine m = new Maschine
                {
                    Id = 0,
                    Seriennummer = "123xyz",
                    Jahrgang = 1999,
                };
                var resultList = maschineManager.GetSearchResult(m);
                Assert.AreEqual(1, resultList.First().Id);
            }
        }

    }
}
