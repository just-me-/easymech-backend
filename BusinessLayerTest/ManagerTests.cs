using EasyMechBackend.BusinessLayer;
using EasyMechBackend.DataAccessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BusinessLayerTest
{
    [TestClass]
    public class ManagerBaseTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(true);
        }
    }

    [TestClass]
    public class KundeManagerTest
    {

        DbContextOptions<EMContext> InitDBwithKundeHelper()
        {
            var options = new DbContextOptionsBuilder<EMContext>()
                            .UseInMemoryDatabase(databaseName: "getKundenTestDB")
                            .Options;

            using (var context = new EMContext(options))
            {
                Kunde k = new Kunde
                {
                    Id = 1,
                    Firma = "Test AG",
                    IsActive = true
                };
                k.Validate();
                context.Add(k);
                context.SaveChanges();
            }

            return options;
        }


        [TestMethod]
        public void AddKundeTest()
        {
            var options = InitDBwithKundeHelper();

            using (var context = new EMContext(options))
            {
                Assert.AreEqual(1, context.Kunden.Count());
                Assert.AreEqual("Test AG", context.Kunden.Single().Firma);
            }
        }

        [TestMethod]
        public void GetKundeByIdTest()
        {
            Assert.IsTrue(true);
        }
    }
}
