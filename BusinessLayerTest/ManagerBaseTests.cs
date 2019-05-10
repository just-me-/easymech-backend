﻿using EasyMechBackend.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

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
