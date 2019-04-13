using EasyMechBackend.DataAccessLayer;
using EasyMechBackend.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                    Email = "t@b.ch",
                    Telefon = "+41 81 123 45 68",
                    Notiz = "Zahlt immer pünktlich, ist ganz nett.\nDarf weider mal eine Maschine mieten"
                };


                Kunde k2 = new Kunde
                {
                    Firma = "DJ Fire",
                    Vorname = "Dario",
                    Nachname = "Fuoco",
                    PLZ = "9475",
                    Ort = "Sevelen",
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
                    Notiz = "Sekretärin rastet aus und hämmert auf Tastatur rum: +*ç%&/()=à£\\\"éàé!!è!è£èè£è{@#°{@°{#°¢°¬¢§¬¬§¬§}°@}",
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

        public static List<Kunde> GetSearchResult(Kunde searchEntity)
        {
            if( searchEntity.Id != 0)
            {
                return new List<Kunde>
                {
                    GetKundeById(searchEntity.Id)
                };

            }


            List<Kunde> allKunden = GetKunden();
            IEnumerable<Kunde> searchResult = allKunden;



            PropertyInfo[] props = typeof(Kunde).GetProperties();

            foreach (var prop in props) {

                //id and isActive are no subject for searching -> these are the only ones with onn-string fields
                if (prop.PropertyType != typeof(string)) continue;


                string potentialSearchTerm = (string)prop.GetValue(searchEntity);
                if (potentialSearchTerm.HasSearchTerm())
                {
                    searchResult = searchResult.Where(k => {
                        string contentOfCustomerThatIsEvaluated = (string)prop.GetValue(k);
                        return contentOfCustomerThatIsEvaluated != null &&
                               contentOfCustomerThatIsEvaluated.Contains(potentialSearchTerm);

                        });
                }
            }

            
            if (searchResult.Any())
            {
                return searchResult.ToList();
            }
            else
            {
                return new List<Kunde>();
            }
            
        }
    }
}
