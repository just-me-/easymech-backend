using EasyMechBackend.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayerTest
{
    
    public class ManagerBaseTests
    {
        public DbContextOptions<EMContext> Options { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            Options = BusinessLayerTestHelper.InitTestDb();
        }
    }
}
