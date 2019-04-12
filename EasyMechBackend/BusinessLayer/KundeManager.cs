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
            if (GetKunden().Count() == 0)
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
                    Notiz =
                    @"Zahlt immer pünktlich, ist ganz nett.
                    Darf weider mal eine Maschine mieten"
                };


                Kunde k2 = new Kunde
                {
                    Firma = "DJ Fire",
                    Vorname = "Dario",
                    Nachname = "Fuoco",
<<<<<<< HEAD
                    Adresse = "Strasse 1",
                    PLZ = "7500",
=======
                    PLZ = 7500,
>>>>>>> parent of cfb01e3... Merge branch 'master' of ssh://gitlab.dev.ifs.hsr.ch:45022/epj-2019-fs/easymech/easymech-backend
                    Ort = "Sargans",
                    Email = "DJ-Fire (at) geilepartysimbunker (dot) com",
                    IsActive = false
                };

                Kunde k3 = new Kunde
                {
                    Firma = "Hack0r",
                    Vorname = "<script>alert(\"Fail\")</script>",
                    Nachname = "O'Brian",
<<<<<<< HEAD
                    Adresse = "Strasse 1",
                    PLZ = "0",
=======
                    PLZ = 0,
>>>>>>> parent of cfb01e3... Merge branch 'master' of ssh://gitlab.dev.ifs.hsr.ch:45022/epj-2019-fs/easymech/easymech-backend
                    Ort = "",
                    Email = "",
                    IsActive = false
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
