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
    public class MaschineManagerTests : ManagerBaseTests
    {        
        [TestMethod]
        public void AddMaschineTest()
        {
            using (var context = new EMContext(Options))
            {
                int id = 3;
                Maschine m = new Maschine
                {
                    Id = id,
                    Seriennummer = "777777",
                    Jahrgang = 1999,
                    IstAktiv = true
                };
                MaschineManager maschineManager = new MaschineManager(context);
                maschineManager.AddMaschine(m);
                var addedMaschine = context.Maschinen.Single(maschine => maschine.Id == id);
                Assert.AreEqual("777777", addedMaschine.Seriennummer);
            }
        }

        [TestMethod]
        public void AddDuplicateMaschine()
        {
            using (var context = new EMContext(Options))
            {
                int id = 3;
                Maschine m = new Maschine
                {
                    Id = id,
                    Seriennummer = "123xyz",
                };
                MaschineManager maschineManager = new MaschineManager(context);

                Assert.ThrowsException<UniquenessException>(() =>
                {
                    maschineManager.AddMaschine(m);
                });
            }
        }

        //[TestMethod]
        //public void AddMaschineIntoDeleteIntoAddAgain()
        //{
        //    var options = ResetDBwithMaschineHelper();
        //    using (var context = new EMContext(options))
        //    {
        //        int id = 2;
        //        Maschine newMaschine = new Maschine
        //        {
        //            Id = id,
        //            Seriennummer = "123xyz!!"
        //        };

        //        MaschineManager man = new MaschineManager(context);
        //        var original = man.GetMaschineById(1);
        //        man.SetMaschineInactive(original);
        //        man.AddMaschine(newMaschine);
        //        var listen = man.GetMaschinen(true);
        //        Assert.AreEqual(2, listen.Count);
        //    }
        //}


        [TestMethod]
        public void GetMaschinenTest()
        {
            using (var context = new EMContext(Options))
            {
                MaschineManager maschineManager = new MaschineManager(context);
                var maschinenList = maschineManager.GetMaschinen(false);
                Assert.AreEqual(2, maschinenList.Count);
            }
        }
        [TestMethod]
        public void GetMaschineByIdTest()
        {
            using (var context = new EMContext(Options))
            {
                MaschineManager maschineManager = new MaschineManager(context);
                var maschine1 = maschineManager.GetMaschineById(1);
                Assert.AreEqual("123xyz", maschine1.Seriennummer);
            }
        }
        [TestMethod]
        public void GetMaschineByNonexistantIdTest()
        {
            using (var context = new EMContext(Options))
            {
                MaschineManager maschineManager = new MaschineManager(context);
                Assert.ThrowsException<InvalidOperationException>(() => maschineManager.GetMaschineById(666));
            }
        }
        [TestMethod]
        public void UpdateMaschineTest()
        {
            using (var context = new EMContext(Options))
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
        public void SetInactiveTest()
        {
            using (var context = new EMContext(Options))
            {
                MaschineManager maschineManager = new MaschineManager(context);
                var original = maschineManager.GetMaschineById(1);
                maschineManager.SetMaschineInactive(original);
                var updated = maschineManager.GetMaschineById(1);
                Assert.IsFalse(updated.IstAktiv ?? true);
            }
        }

        [TestMethod]
        public void DeleteMaschineTest()
        {
            using (var context = new EMContext(Options))
            {
                MaschineManager maschineManager = new MaschineManager(context);
                var maschine = maschineManager.GetMaschineById(1);
                maschineManager.DeleteMaschine(maschine);
                Assert.AreEqual(1, context.Maschinen.Count());
            }
        }


        [TestMethod]
        public void GetSearchResultMaschineTest()
        {
            using (var context = new EMContext(Options))
            {
                MaschineManager maschineManager = new MaschineManager(context);
                Maschine m = new Maschine
                {
                    Id = 1,
                    Seriennummer = "123xyz",
                    Jahrgang = 1999,
                };
                var resultList = maschineManager.GetSearchResult(m);
                Assert.AreEqual(1, resultList.First().Id);
            }
        }

    }
}
