using EasyMechBackend.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.BusinessLayer
{
    public class KundeManager : ManagerBase
    {

        //Fill Dummy Data for Dev
        #region dummydata
        //Enter a random Data
        static KundeManager()
        {
            if (GetKunden().Count() == 0)
            {
                Kunde k1 = new Kunde
                {
                    Firma = "Toms Vergnügungspark",
                    Vorname = "Tom",
                    Nachname = "K",
                    PLZ = 7000,
                    Ort = "Chur",
                    Email = "t-kistler@bluewin.ch",
                    Telefon = "081 123 45 68",
                    Notiz =
                    @"Zahlt immer pünktlich, ist ganz nett.
                    Darf weider mal eine Maschine mieten"
                };


                Kunde k2 = new Kunde
                {
                    Firma = "DJ Fire",
                    Vorname = "Dario",
                    Nachname = "Fuoco",
                    PLZ = 7500,
                    Ort = "Sargans",
                    Email = "DJ-Fire (at) geilepartysimbunker (dot) com",
                    IsActive = false
                };

                AddKunde(k1);
                AddKunde(k2);
            }
        }

        #endregion
        

        public static List<Kunde> GetKunden()
        {
            using (EMContext c = new EMContext())
            {
                return c.Kunden.ToList();
            }
        }

        public static Kunde GetKundeById(long id)
        {
            using (EMContext c = new EMContext())
            {
                return c.Kunden.SingleOrDefault(kunde => kunde.Id == id);
            }
        }

        public static Kunde AddKunde(Kunde k)
        {
            using (EMContext c = new EMContext())
            {
                c.Add(k);
                c.SaveChanges();
                return k;
            }
        }

        public static Kunde UpdateKunde(Kunde k)
        {
            using (EMContext c = new EMContext())
            {
                c.Entry(k).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                c.SaveChanges();
                return k;
            }
        }

        public static void DeleteKunde(Kunde k)
        {
            using (EMContext c = new EMContext())
            {
                c.Remove(k);
                c.SaveChanges();
            }
        }
    }
}
