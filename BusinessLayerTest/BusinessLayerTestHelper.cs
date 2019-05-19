using EasyMechBackend.BusinessLayer;
using EasyMechBackend.DataAccessLayer;
using EasyMechBackend.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using static EasyMechBackend.Common.EnumHelper;

namespace BusinessLayerTest
{
    static class BusinessLayerTestHelper
    {
        public static DbContextOptions<EMContext> InitTestDb()
        {
            var options = new DbContextOptionsBuilder<EMContext>()
                            .UseInMemoryDatabase(databaseName: "TestDB")
                            .Options;

            using (var context = new EMContext(options))
            {
                ReservationManager resManager = new ReservationManager(context);
                TransaktionManager transaktionManager = new TransaktionManager(context);
                KundeManager kundeManager = new KundeManager(context);
                MaschineManager maschineManager = new MaschineManager(context);
                MaschinentypManager typManager = new MaschinentypManager(context);
                ServiceManager serviceManager = new ServiceManager(context);


                foreach (Reservation r in resManager.GetReservationen())
                {
                    context.Remove(r);
                }
                foreach (Transaktion t in transaktionManager.GetTransaktionen())
                {
                    context.Remove(t);
                }
                foreach (Kunde k in kundeManager.GetKunden(true))
                {
                    context.Remove(k);
                }
                foreach (Maschine m in maschineManager.GetMaschinen(true))
                {
                    context.Remove(m);
                }
                foreach (Maschinentyp typ in typManager.GetMaschinentypen())
                {
                    context.Remove(typ);
                }
                foreach (Service s in serviceManager.GetServices(0))
                {
                    context.Remove(s);
                }

                context.SaveChanges();
                Kunde k1 = new Kunde
                {
                    Id = 1,
                    Firma = "duko Stapler",
                    Vorname = "",
                    Nachname = "",
                    PLZ = "",
                    Ort = "",
                    Email = "",
                    Telefon = "",
                    Notiz = ""
                };
                k1.Validate();
                Kunde k2 = new Kunde
                {
                    Id = 2,
                    Firma = "Unser erster Kunde",
                    Vorname = "Ben",
                    Nachname = "Stark",
                    PLZ = "9010",
                    Ort = "St.Gallen",
                    Email = "test2@b.ch",
                    Telefon = "+41 11 133 45 48",
                    Notiz = "Winter is coming"
                };
                k2.Validate();

                Maschinentyp t1 = new Maschinentyp
                {
                    Id = 1,
                    Fabrikat = "Tester grande",
                    Nutzlast = 2000
                };
                t1.Validate();

                Maschine m1 = new Maschine
                {
                    Id = 1,
                    Seriennummer = "123xyz",
                    Jahrgang = 1999,
                    IstAktiv = true,
                    Besitzer = k1,
                    Maschinentyp = t1
                };
                m1.Validate();

                Maschine m2 = new Maschine
                {
                    Id = 2,
                    Seriennummer = "456abc",
                    Jahrgang = 2011,
                    IstAktiv = true,
                    Besitzer = k2,
                    Maschinentyp = t1                    
                };
                m2.Validate();

                Transaktion startTransaktionEinkauf = new Transaktion
                {
                    Id = 1,
                    Preis = 50000,
                    Typ = Transaktion.TransaktionsTyp.Einkauf,
                    Datum = new DateTime(2019, 05, 15),
                    MaschinenId = 1,
                    KundenId = 1
                };

                Transaktion startTransaktionVerkauf = new Transaktion
                {
                    Id = 2,
                    Preis = 45000,
                    Typ = Transaktion.TransaktionsTyp.Verkauf,
                    Datum = new DateTime(2019, 05, 16),
                    MaschinenId = 2,
                    KundenId = 1
                };

                Reservation r1 = new Reservation
                {
                    Id = 1,
                    Standort = "Chur",
                    Startdatum = new DateTime(2019, 05, 10),
                    Enddatum =   new DateTime(2019, 05, 12),
                    MaschinenId = 1,
                    KundenId = 2
                };

                Reservation r2 = new Reservation
                {
                    Id = 2,
                    Standort = "In Tümpel gefahren",
                    Startdatum = new DateTime(2019, 05, 14),
                    Enddatum = new DateTime(2019, 05, 16),
                    MaschinenId = 1,
                    KundenId = 1,
                    Uebergabe = new MaschinenUebergabe()
                };

                Service s1 = new Service
                {
                    Id = 1,
                    Bezeichnung = "Oelwechsel",
                    Beginn = new DateTime(2020, 01, 12),
                    Ende = new DateTime(2020, 01, 13),
                    Status = ServiceState.Pending,
                    MaschinenId = 1,
                    KundenId = 1,
                    Materialposten = new List<Materialposten>(),
                    Arbeitsschritte = new List<Arbeitsschritt>()                    
                };

                Service s2 = new Service
                {
                    Id = 2,
                    Bezeichnung = "Oelwechsel",
                    Beginn = new DateTime(2019, 05, 15),
                    Ende = new DateTime(2019, 05, 20),
                    Status = ServiceState.Running,
                    MaschinenId = 1,
                    KundenId = 1,
                    Materialposten = new List<Materialposten>(),
                    Arbeitsschritte = new List<Arbeitsschritt>()
                };

                Service s3 = new Service
                {
                    Id = 3,
                    Bezeichnung = "Oelwechsel",
                    Beginn = new DateTime(2019, 01, 17),
                    Ende = new DateTime(2019, 01, 19),
                    Status = ServiceState.Completed,
                    MaschinenId = 1,
                    KundenId = 1,
                    Materialposten = new List<Materialposten>(),
                    Arbeitsschritte = new List<Arbeitsschritt>()
                };

                context.Add(k1);
                context.Add(k2);
                context.Add(t1);
                context.Add(m1);
                context.Add(m2);
                context.Add(startTransaktionEinkauf);
                context.Add(startTransaktionVerkauf);
                context.Add(r1);
                context.Add(r2);
                context.Add(s1);
                context.Add(s2);
                context.Add(s3);
                context.SaveChanges();
            }
            return options;
        }
    }
}
