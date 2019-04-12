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
        static KundeManager()
        {
            if (!GetKunden().Any())
            {
                Kunde k1 = new Kunde
                {
                    Firma = "Toms Vergnügungspark",
                    Vorname = "Tom",
                    Nachname = "K",
                    PLZ = "7000",
                    Ort = "Chur",
                    Email = "t-kistler@bluewin.ch",
                    Telefon = "081 123 45 68",
                    Notiz = "Zahlt immer pünktlich, ist ganz nett.\nDarf weider mal eine Maschine mieten"
                };


                Kunde k2 = new Kunde
                {
                    Firma = "DJ Fire",
                    Vorname = "Dario",
                    Nachname = "Fuoco",
                    PLZ = "7500",
                    Ort = "Sargans",
                    Email = "DJ-Fire (at) geilepartysimbunker (dot) com",
                    IsActive = true
                };

                Kunde k3 = new Kunde
                {
                    Firma = "Hack0r",
                    Vorname = "<script>alert(\"Fail\")</script>",
                    Nachname = "O'Brian",
                    PLZ = "0",
                    Ort = "",
                    Email = "",
                    IsActive = true
                };

                AddKunde(k1);
                AddKunde(k2);
                AddKunde(k3);
            }
        }

        #endregion
        
        public static List<Kunde> GetKunden()
        {
            using (EMContext c = new EMContext())
            {
                var query =
                from k in c.Kunden
                where k.IsActive.Value
                orderby k.Id descending
                select k;
                return query.ToList();
            }
        }

        public static Kunde GetKundeById(long id)
        {
            using (EMContext c = new EMContext())
            {
                Kunde k = c.Kunden.SingleOrDefault(kunde => kunde.Id == id);
                if (k == null)
                {
                    throw new InvalidOperationException($"Kunde with id {id} is not in database");
                }
                return k;
            }
        }

        public static Kunde AddKunde(Kunde k)
        {
            using (EMContext c = new EMContext())
            {
                k.Validate();
                c.Add(k);
                c.SaveChanges();
                return k;
            }
        }

        public static Kunde UpdateKunde(Kunde k)
        {
            using (EMContext c = new EMContext())
            {
                k.Validate();
                c.Entry(k).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                c.SaveChanges();
                return k;
            }
        }

        public static void SetKundeInactive(Kunde k)
        {
            using (EMContext c = new EMContext())
            {
                k.IsActive = false;
                UpdateKunde(k);
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
