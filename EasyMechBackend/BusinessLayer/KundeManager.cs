using EasyMechBackend.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.BusinessLayer
{
    public class KundeManager : ManagerBase
    {
        public static List<Kunde> GetKunden()
        {
            using (EMContext c = new EMContext())
            {
                return c.Kunden.ToList();
            }
        }

        public static Kunde GetKundeById(int id)
        {
            using (EMContext c = new EMContext())
            {
                return c.Kunden.SingleOrDefault(kunde => kunde.Id == id);
            }
        }

        public static void AddKunde(Kunde k)
        {
            using (EMContext c = new EMContext())
            {
                c.Add(k);
                c.SaveChanges();
            }
        }

        public static void UpdateKunde(Kunde k)
        {
            using (EMContext c = new EMContext())
            {
                c.Entry(k).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                c.SaveChanges();
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
