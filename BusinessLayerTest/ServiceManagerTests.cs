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

namespace BusinessLayerTest
{
    [TestClass]
    public class ServiceManagerDefaultCasesTests : ManagerBaseTests
    {
        [TestMethod]
        public void GetReservationenTest()
        {
            using (var context = new EMContext(options))
            {
                ReservationManager man = new ReservationManager(context);
                var list = man.GetReservationen();
                Assert.AreEqual(2, list.Count);
            }
        }

        [TestMethod]
        public void GetSingleReservationTest()
        {
            using (var context = new EMContext(options))
            {
                ReservationManager man = new ReservationManager(context);
                var res = man.GetReseervationById(1);
                Assert.AreEqual(1, res.Id);
                Assert.AreEqual("Chur", res.Standort);
                Assert.IsNull(res.Uebergabe);
                Assert.IsNull(res.Ruecknahme);

            }
        }


        [TestMethod]
        public void AddReservation_NoU_NoR()
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
                    KundenId = 2
                };
                var man = new ReservationManager(context);
                man.AddReservation(e);
                var addedEntity = context.Reservationen.Single(res => res.Id == id);
                Assert.AreEqual(id, addedEntity.Id);
                Assert.IsNull(addedEntity.Uebergabe);
                Assert.IsNull(addedEntity.Ruecknahme);
            }
        }


        [TestMethod]
        public void AddReservation_WithU_NoR()
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
                    Uebergabe = new MaschinenUebergabe
                    {
                        Datum = new DateTime(2030, 05, 12)
                    }
                };

                var man = new ReservationManager(context);
                man.AddReservation(e);
                var addedEntity = context.Reservationen.Single(res => res.Id == id);
                Assert.AreEqual(id, addedEntity.Id);
                Assert.IsNotNull(addedEntity.Uebergabe);
                Assert.IsNull(addedEntity.Ruecknahme);
            }
        }

        [TestMethod]
        public void AddReservation_WithU_WithR()
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
                    Uebergabe = new MaschinenUebergabe
                    {
                        Datum = new DateTime(2030, 05, 12)
                    },
                    Ruecknahme = new MaschinenRuecknahme
                    {
                        Datum = new DateTime(2040, 05, 13)
                    }
                };

                var man = new ReservationManager(context);
                man.AddReservation(e);
                var addedEntity = context.Reservationen.Single(res => res.Id == id);
                Assert.AreEqual(id, addedEntity.Id);
                Assert.IsNotNull(addedEntity.Uebergabe);
                Assert.IsNotNull(addedEntity.Ruecknahme);
            }
        }


        [TestMethod]
        public void UpdateReservation()
        {
            using (var context = new EMContext(options))
            {
                var man = new ReservationManager(context);
                var original = man.GetReseervationById(1);
                original.Standort = "Rappi";

                //Snippet not working in In Memory DB as Nav Props dont load properly. Tested in Postman.

                //original.Uebergabe = new MaschinenUebergabe
                //{
                //    Id = 12,
                //    Datum = new DateTime(2020, 1, 1)
                //};
                //original.Ruecknahme = null;


                man.UpdateReservation(original);
                var updated = man.GetReseervationById(1);

                Assert.AreEqual("Rappi", updated.Standort);
                //Assert.IsNotNull(updated.Uebergabe);
                //Assert.IsNull(updated.Ruecknahme);
            }
        }


        [TestMethod]
        public void DeleteTest()
        {
            using (var context = new EMContext(options))
            {
                var man = new ReservationManager(context);

                var doomed = man.GetReseervationById(1);
                man.DeleteReservation(doomed);

                doomed = man.GetReseervationById(2);
                man.DeleteReservation(doomed);

                var list = man.GetReservationen();
                Assert.IsFalse(list.Any());

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
                    Id = id+1,
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
                var added = man.GetReseervationById(3);
                Assert.AreEqual(a, added.Uebergabe.Datum);
                Assert.AreEqual(b, added.Ruecknahme.Datum);
            }
        }
    }
}
