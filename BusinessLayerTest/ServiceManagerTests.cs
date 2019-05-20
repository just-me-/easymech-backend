using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using EasyMechBackend.DataAccessLayer;
using EasyMechBackend.BusinessLayer;
using System.Linq;
using EasyMechBackend.Common.Exceptions;
using EasyMechBackend.DataAccessLayer.Entities;
using EasyMechBackend.Common;
using static EasyMechBackend.Common.EnumHelper;
using System.Collections.Generic;

namespace BusinessLayerTest
{
    [TestClass]
    public class ServiceManagerDefaultCasesTests : ManagerBaseTests
    {
        [TestMethod]
        public void GetAllServicesTest()
        {
            using (var context = new EMContext(options))
            {
                ServiceManager man = new ServiceManager(context);
                var list = man.GetServices(ServiceState.All);
                Assert.AreEqual(3, list.Count);
            }
        }

        [TestMethod]
        public void GetPendingServicesTest()
        {
            using (var context = new EMContext(options))
            {
                ServiceManager man = new ServiceManager(context);
                var list = man.GetServices(ServiceState.Pending);
                Assert.AreEqual(1, list.First().Id);
            }
        }

        [TestMethod]
        public void GetRunningServicesTest()
        {
            using (var context = new EMContext(options))
            {
                ServiceManager man = new ServiceManager(context);
                var list = man.GetServices(ServiceState.Running);
                Assert.AreEqual(2, list.First().Id);
            }
        }

        [TestMethod]
        public void GetCompletetServicesTest()
        {
            using (var context = new EMContext(options))
            {
                ServiceManager man = new ServiceManager(context);
                var list = man.GetServices(ServiceState.Completed);
                Assert.AreEqual(3, list.First().Id);
            }
        }

        [TestMethod]
        public void GetServiceByIdTest()
        {
            using (var context = new EMContext(options))
            {
                ServiceManager man = new ServiceManager(context);
                var res = man.GetServiceById(1);
                Assert.AreEqual(1, res.Id);
            }
        }

        [TestMethod]
        public void GetServiceByNonexistantIdTest()
        {
            using (var context = new EMContext(options))
            {
                ServiceManager man = new ServiceManager(context);
                Assert.ThrowsException<InvalidOperationException>(() => man.GetServiceById(999));
            }
        }

        [TestMethod]
        public void AddReservationWithoutMaterialOrArbeit()
        {
            using (var context = new EMContext(options))
            {
                int id = 4;
                Service s = new Service
                {
                    Id = id,
                    Bezeichnung = "Added Service",
                    Beginn = new DateTime(2019, 06, 17),
                    Ende = new DateTime(2019, 07, 19),
                    Status = ServiceState.Pending,
                    MaschinenId = 1,
                    KundenId = 1
                };
                var man = new ServiceManager(context);
                man.AddService(s);
                var addedEntity = context.Services.Single(res => res.Id == id);
                Assert.AreEqual(id, addedEntity.Id);
                Assert.IsTrue(!addedEntity.Materialposten.Any());
                Assert.IsTrue(!addedEntity.Arbeitsschritte.Any());
            }
        }

        [TestMethod]
        public void AddServiceWithMaterialAndArbeit()
        {
            using (var context = new EMContext(options))
            {
                int id = 4;
                Service s = new Service
                {
                    Id = id,
                    Bezeichnung = "Added Service",
                    Beginn = new DateTime(2019, 06, 17),
                    Ende = new DateTime(2019, 07, 19),
                    Status = ServiceState.Pending,
                    MaschinenId = 1,
                    KundenId = 1,
                    Arbeitsschritte = new List<Arbeitsschritt>
                    {
                        new Arbeitsschritt
                        {
                            Bezeichnung = "Schraube Anziehen"
                        },
                        new Arbeitsschritt
                        {
                            Bezeichnung = "noch fester Anziehen"
                        }
                    },
                    Materialposten = new List<Materialposten>
                    {
                        new Materialposten
                        {
                            Bezeichnung = "Schraube t64"
                        }
                    }
                };
                var man = new ServiceManager(context);
                man.AddService(s);
                var addedEntity = context.Services.Single(res => res.Id == id);
                Assert.AreEqual(id, addedEntity.Id);
                Assert.AreEqual(1, addedEntity.Materialposten.Count);
                Assert.AreEqual(2, addedEntity.Arbeitsschritte.Count);
            }
        }
     
        [TestMethod]
        public void UpdateServiceSimple()
        {
            using (var context = new EMContext(options))
            {
                var man = new ServiceManager(context);
                var original = man.GetServiceById(1);
                original.Bezeichnung = "Updated Service";

                man.UpdateService(original);
                var updated = man.GetServiceById(1);

                Assert.AreEqual("Updated Service", updated.Bezeichnung);
            }
        }

        [TestMethod]
        public void DeleteServiceTest()
        {
            using (var context = new EMContext(options))
            {
                var man = new ServiceManager(context);

                var doomed = man.GetServiceById(3);
                man.DeleteService(doomed);

                var list = man.GetServices(0);
                Assert.AreEqual(2, list.Count);
            }
        }
    }



    [TestClass]
    public class ServiceManagerSpecialCasesTests : ManagerBaseTests
    {
        [TestMethod]
        public void AddService_EndBeforeStart()
        {
            using (var context = new EMContext(options))
            {
                int id = 4;
                Service s = new Service
                {
                    Id = id,
                    Bezeichnung = "Added Service",
                    Beginn = new DateTime(2019, 06, 17),
                    Ende = new DateTime(2019, 06, 16),
                    Status = ServiceState.Pending,
                    MaschinenId = 1,
                    KundenId = 1
                };

                var man = new ServiceManager(context);
                Assert.ThrowsException<MaintenanceException>(() => man.AddService(s));
            }
        }

        [TestMethod]
        public void AddService_DateOverlap1_ExactDateMatch()
        {
            using (var context = new EMContext(options))
            {
                int id = 4;
                Service s = new Service
                {
                    Id = id,
                    Bezeichnung = "Added Service",
                    Beginn = new DateTime(2020, 01, 12),
                    Ende = new DateTime(2020, 01, 13),
                    Status = ServiceState.Pending,
                    MaschinenId = 1,
                    KundenId = 1
                };

                var man = new ServiceManager(context);
                Assert.ThrowsException<MaintenanceException>(() => man.AddService(s));
            }
        }

        [TestMethod]
        public void AddService_DateOverlap2_OutsideDate()
        {
            using (var context = new EMContext(options))
            {
                int id = 4;
                Service s = new Service
                {
                    Id = id,
                    Bezeichnung = "Added Service",
                    Beginn = new DateTime(2000, 01, 12),
                    Ende = new DateTime(2050, 01, 13),
                    Status = ServiceState.Pending,
                    MaschinenId = 1,
                    KundenId = 1
                };

                var man = new ServiceManager(context);
                Assert.ThrowsException<MaintenanceException>(() => man.AddService(s));
            }
        }

        [TestMethod]
        public void AddService_DateOverlap3_InsideDate()
        {
            using (var context = new EMContext(options))
            {
                int id = 4;
                Service s = new Service
                {
                    Id = id,
                    Bezeichnung = "Added Service",
                    Beginn = new DateTime(2019, 05, 16),
                    Ende = new DateTime(2019, 05, 19),
                    Status = ServiceState.Pending,
                    MaschinenId = 1,
                    KundenId = 1
                };

                var man = new ServiceManager(context);
                Assert.ThrowsException<MaintenanceException>(() => man.AddService(s));
            }
        }

        [TestMethod]
        public void AddService_DateOverlap_ButDifferentMachine()
        {
            {
                using (var context = new EMContext(options))
                {
                    int id = 4;
                    Service s = new Service
                    {
                        Id = id,
                        Bezeichnung = "Added Service",
                        Beginn = new DateTime(2000, 01, 12),
                        Ende = new DateTime(2050, 01, 13),
                        Status = ServiceState.Pending,
                        MaschinenId = 2,
                        KundenId = 1
                    };

                    var man = new ServiceManager(context);
                    man.AddService(s);
                    Assert.AreEqual(4, context.Services.Single(serv => serv.Id == 4).Id);
                }
            }
        }
    }
}
