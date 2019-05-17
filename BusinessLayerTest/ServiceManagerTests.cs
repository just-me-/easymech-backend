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

                var doomed = man.GetServiceById(1);
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
        public void AddReservation_NoU_WithR_MustThrow()
        {
            using (var context = new EMContext(options))
            {
                int id = 3;
                Reservation e = new Reservation
                {
                    Id = id,
                    Standort = "Chur",
                    Startdatum = new DateTime(2030, 05, 12),
                    Enddatum = new DateTime(2040, 05, 12),
                    MaschinenId = 1,
                    KundenId = 2,
                    Ruecknahme = new MaschinenRuecknahme
                    {
                        Datum = new DateTime(2040, 05, 13)
                    }
                };

                var man = new ReservationManager(context);
                Assert.ThrowsException<ReservationException>(() => man.AddReservation(e));
            }
        }


        [TestMethod]
        public void AddReservation_Return_before_Take()
        {
            using (var context = new EMContext(options))
            {
                int id = 3;
                Reservation e = new Reservation
                {
                    Id = id,
                    Standort = "Chur",
                    Startdatum = new DateTime(2040, 05, 12),
                    Enddatum = new DateTime(2030, 05, 12),
                    MaschinenId = 1,
                    KundenId = 2
                };

                var man = new ReservationManager(context);
                Assert.ThrowsException<ReservationException>(() => man.AddReservation(e));
            }
        }

        [TestMethod]
        public void AddReservation_DateOverlap()
        {
            using (var context = new EMContext(options))
            {
                int id = 3;
                Reservation e = new Reservation
                {
                    Id = id,
                    Standort = "Chur",
                    Startdatum = new DateTime(2000, 08, 12),
                    Enddatum = new DateTime(2019, 05, 13),
                    MaschinenId = 1,
                    KundenId = 2
                };

                var man = new ReservationManager(context);
                Assert.ThrowsException<ReservationException>(() => man.AddReservation(e));
            }
        }


        [TestMethod]
        public void AddReservation_DateOverlap_ButDifferentMachine()
        {
            using (var context = new EMContext(options))
            {
                long id = 12;
                Maschine m5 = new Maschine
                {
                    Id = id,
                    Seriennummer = "my machine 2",
                    BesitzerId = 1
                };

                var man_masch = new MaschineManager(context);
                man_masch.AddMaschine(m5);

                Reservation e = new Reservation
                {
                    Id = id,
                    Standort = "Chur",
                    Startdatum = new DateTime(2019, 05, 12), //same as in machine 1
                    Enddatum = new DateTime(2020, 05, 12),
                    MaschinenId = id,
                    KundenId = 2
                };

                var man = new ReservationManager(context);
                man.AddReservation(e);
                Assert.AreEqual(3, man.GetReservationen().Count);
            }
        }


        [TestMethod]
        public void AddReservation_OpenEnd()
        {
            using (var context = new EMContext(options))
            {
                int id = 3;
                Reservation e = new Reservation
                {
                    Id = id,
                    Standort = "Chur",
                    Startdatum = new DateTime(2030, 01, 11),
                    MaschinenId = 1,
                    KundenId = 2
                };

                var man = new ReservationManager(context);
                man.AddReservation(e);

                Reservation e2 = new Reservation
                {
                    Id = id + 1,
                    Standort = "Chur",
                    Startdatum = new DateTime(2050, 01, 11),
                    Enddatum = new DateTime(2060, 01, 11),
                    MaschinenId = 1,
                    KundenId = 2
                };

                Assert.ThrowsException<ReservationException>(() => man.AddReservation(e2));

            }
        }

        [TestMethod]
        public void AddReservation_NotInOwnProperty()
        {
            using (var context = new EMContext(options))
            {
                long id = 12;
                Maschine m5 = new Maschine
                {
                    Id = id,
                    Seriennummer = "owner 2",
                    BesitzerId = 2
                };

                var man_masch = new MaschineManager(context);
                man_masch.AddMaschine(m5);

                Reservation e = new Reservation
                {
                    Id = 3,
                    Startdatum = new DateTime(2030, 05, 12),
                    Enddatum = new DateTime(2040, 05, 12),
                    MaschinenId = id,
                    KundenId = 2
                };

                var man = new ReservationManager(context);
                Assert.ThrowsException<ReservationException>(() => man.AddReservation(e));
            }
        }

        [TestMethod]
        public void AddReservation_Overwrite_Date_When_PickupOrReturn()
        {
            using (var context = new EMContext(options))
            {

                var a = new DateTime(2030, 1, 1);
                var b = new DateTime(2030, 1, 2);
                Reservation e = new Reservation
                {
                    Id = 3,
                    Standort = "Chur",
                    Startdatum = new DateTime(2019, 05, 12),
                    Enddatum = new DateTime(2018, 05, 12),
                    MaschinenId = 1,
                    KundenId = 2,
                    Uebergabe = new MaschinenUebergabe
                    {
                        Datum = a
                    },
                    Ruecknahme = new MaschinenRuecknahme
                    {
                        Datum = b
                    }
                };

                var man = new ReservationManager(context);
                man.AddReservation(e);
                var added = man.GetReservationById(3);
                Assert.AreEqual(a, added.Uebergabe.Datum);
                Assert.AreEqual(b, added.Ruecknahme.Datum);
            }
        }
    }
}
