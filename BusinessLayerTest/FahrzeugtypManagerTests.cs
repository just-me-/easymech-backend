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
        private DbContextOptions<EMContext> InitDBwithFahrzeugtypHelper()
        {
            var options = new DbContextOptionsBuilder<EMContext>()
                            .UseInMemoryDatabase(databaseName: "FahrzeugtypTestDB")
                            .Options;

            using (var context = new EMContext(options))
            {
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
            var options = InitDBwithFahrzeugtypHelper();
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
    }
}
