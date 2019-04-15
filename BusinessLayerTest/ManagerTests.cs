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


        //[TestMethod]
        //public void GetKundenTest()
        //{
        //    var options = new DbContextOptionsBuilder<EMContext>()
        //                    .UseInMemoryDatabase(databaseName: "GetKunden")
        //                    .Options;


        //}
    }
}
