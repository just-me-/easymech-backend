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
    public class FahrzeugtypManagerTests
    {
        private DbContextOptions<EMContext> ResetDBwithFahrzeugtypHelper()
        {
            var options = new DbContextOptionsBuilder<EMContext>()
                            .UseInMemoryDatabase(databaseName: "FahrzeugtypTestDB")
                            .Options;

            using (var context = new EMContext(options))
            {
                FahrzeugtypManager fahrzeugtypManager = new FahrzeugtypManager(context);
                foreach (Fahrzeugtyp t in fahrzeugtypManager.GetFahrzeugtypen())
                {
                    context.Remove(t);
                }
                context.SaveChanges();
                Fahrzeugtyp f = new Fahrzeugtyp
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
        public void AddFahrzeugtypTest()
        {
            var options = ResetDBwithFahrzeugtypHelper();
            using (var context = new EMContext(options))
            {
                int id = 2;
                Fahrzeugtyp f = new Fahrzeugtyp
                {
                    Id = id,
                    Fabrikat = "Tester grande",
                    Nutzlast = 2000
                };
                FahrzeugtypManager fahrzeugtypManager = new FahrzeugtypManager(context);
                fahrzeugtypManager.AddFahrzeugtyp(f);
                var addedFahrzeugtyp = context.Fahrzeugtypen.Single(fahrzeugtyp => fahrzeugtyp.Id == id);
                Assert.IsTrue(addedFahrzeugtyp.Fabrikat.Equals("Tester grande"));
            }
        } 
        [TestMethod]
        public void GetFahrzeugtypenTest()
        {
            var options = ResetDBwithFahrzeugtypHelper();
            using (var context = new EMContext(options))
            {
                FahrzeugtypManager fahrzeugtypManager = new FahrzeugtypManager(context);
                var fahrzeugtypenList = fahrzeugtypManager.GetFahrzeugtypen();
                Assert.AreEqual(1, fahrzeugtypenList.Count);
            }
        }
        [TestMethod]
        public void GetFahrzeugtypByIdTest()
        {
            var options = ResetDBwithFahrzeugtypHelper();
            using (var context = new EMContext(options))
            {
                FahrzeugtypManager fahrzeugtypManager = new FahrzeugtypManager(context);
                var fahrzeugtyp1 = fahrzeugtypManager.GetFahrzeugtypById(1);
                Assert.AreEqual("Tester grande", fahrzeugtyp1.Fabrikat);
            }
        }
        [TestMethod]
        public void GetFahrzeugtypByNonexistantIdTest()
        {
            var options = ResetDBwithFahrzeugtypHelper();
            using (var context = new EMContext(options))
            {
                FahrzeugtypManager fahrzeugtypManager = new FahrzeugtypManager(context);
                Assert.ThrowsException<InvalidOperationException>(() => fahrzeugtypManager.GetFahrzeugtypById(666));
            }
        }
        [TestMethod]
        public void UpdateFahrzeugtypTest()
        {
            var options = ResetDBwithFahrzeugtypHelper();
            using (var context = new EMContext(options))
            {
                FahrzeugtypManager fahrzeugtypManager = new FahrzeugtypManager(context);
                var originalTyp = fahrzeugtypManager.GetFahrzeugtypById(1);
                originalTyp.Fabrikat = "Updated Fabrikat";
                fahrzeugtypManager.UpdateFahrzeugtyp(originalTyp);
                var updatedTyp = fahrzeugtypManager.GetFahrzeugtypById(1);
                Assert.AreEqual("Updated Fabrikat", updatedTyp.Fabrikat);
            }
        }

        [TestMethod]
        public void DeleteFahrzeugtypTest()
        {
            var options = ResetDBwithFahrzeugtypHelper();
            using (var context = new EMContext(options))
            {
                FahrzeugtypManager fahrzeugtypManager = new FahrzeugtypManager(context);
                var originalTyp = fahrzeugtypManager.GetFahrzeugtypById(1);
                fahrzeugtypManager.DeleteFahrzeugtyp(originalTyp);
                Assert.IsTrue(!context.Fahrzeugtypen.Any());
            }
        }

        [TestMethod]
        public void GetSearchResultFahrzeugtypTest()
        {
            var options = ResetDBwithFahrzeugtypHelper();
            using (var context = new EMContext(options))
            {
                FahrzeugtypManager fahrzeugtypManager = new FahrzeugtypManager(context);
                Fahrzeugtyp f = new Fahrzeugtyp
                {
                    Id = 0,
                    Fabrikat = "Tester grande",
                    Nutzlast = 2000
                };
                var resultList = fahrzeugtypManager.GetSearchResult(f);
                Assert.AreEqual(1, resultList.First().Id);
            }
        }

    }
}
