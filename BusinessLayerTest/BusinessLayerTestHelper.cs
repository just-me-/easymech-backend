using EasyMechBackend.BusinessLayer;
using EasyMechBackend.DataAccessLayer;
using EasyMechBackend.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;

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
                    Startdatum = new DateTime(2019, 05, 12),
                    Enddatum =   new DateTime(2020, 05, 12),
                    MaschinenId = 1,
                    KundenId = 2
                };



                context.Add(k1);
                context.Add(k2);
                context.Add(t1);
                context.Add(m1);
                context.Add(m2);
                context.Add(startTransaktionEinkauf);
                context.Add(startTransaktionVerkauf);
                context.Add(r1);
                context.SaveChanges();
            }
            return options;
        }
    }
}
