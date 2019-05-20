using System;
using EasyMechBackend.DataAccessLayer.Entities;
using EasyMechBackend.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DALTest
{
    [TestClass]
    public class ReservationsTest : DalBase
    {
        [TestMethod]
        public void ValidateSimple()
        {
            Reservation reservation = new Reservation
            {
                Id = 1,
                Standort = text,
                Startdatum = new DateTime(2019, 05, 12),
                Enddatum = new DateTime(2020, 05, 12),
                MaschinenId = 2,
                KundenId = 2
            };

            Reservation expected = new Reservation
            {
                Id = 1,
                Standort = text,
                Startdatum = new DateTime(2019, 05, 12),
                Enddatum = new DateTime(2020, 05, 12),
                MaschinenId = 2,
                KundenId = 2,
                Uebergabe = null,
                Ruecknahme = null
            };

            reservation.Validate();

            Assert.IsTrue(HaveSameData(expected, reservation));
        }

        [TestMethod]
        public void ValidateTooLong()
        {
            Reservation reservation = new Reservation
            {
                Id = 1,
                Standort = longText + longText,
                Startdatum = new DateTime(2019, 05, 12),
                Enddatum = new DateTime(2020, 05, 12),
                MaschinenId = 2,
                KundenId = 2
            };

            Reservation expected = new Reservation();
            expected.Id = 1;
            expected.Standort = (longText + longText).ClipToNChars(256);
            expected.Startdatum = new DateTime(2019, 05, 12);
            expected.Enddatum = new DateTime(2020, 05, 12);
            expected.MaschinenId = 2;
            expected.KundenId = 2;
            expected.Uebergabe = null;
            expected.Ruecknahme = null;

            reservation.Validate();

            Assert.IsTrue(HaveSameData(expected, reservation));
        }
    }
}

