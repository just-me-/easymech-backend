using EasyMechBackend.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLayerTest
{
    public class ManagerBaseTests
    {
        public DbContextOptions<EMContext> options;

        [TestInitialize]
        public void TestInitialize()
        {
            options = BusinessLayerTestHelper.InitTestDb();
        }
    }
}
