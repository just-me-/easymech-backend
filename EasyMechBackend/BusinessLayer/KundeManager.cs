using EasyMechBackend.DataAccessLayer;
using EasyMechBackend.Util;
using Microsoft.EntityFrameworkCore;
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
        public KundeManager(EMContext context)
        {
            Context = context;

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
                    IstAktiv = true
                };

                Kunde k3 = new Kunde
                {
                    Firma = "Hack0r",
                    Vorname = "<script>alert(\"Fail\")</script>",
                    Nachname = "O'Brian",
                    Adresse = "Strasse 1",
                    PLZ = "0",
                    Ort = "",
                    Notiz = "Sekretärin rastet aus und hämmert auf Tastatur rum: +*ç%&/()=à£\\\"éàé!!è!è£èè£è{@#°{@°{#°¢°¬¢§¬¬§¬§}°@}",
                    IstAktiv = true
                };

                AddKunde(k1);
                AddKunde(k2);
                AddKunde(k3);
            }
        }

        #endregion

        public List<Kunde> GetKunden()
        {
            var query =
            from k in Context.Kunden
            where k.IstAktiv.Value
            orderby k.Id descending
            select k;
            return query.ToList();
        }

        public Kunde GetKundeById(long id)
        {
            Kunde k = Context.Kunden.SingleOrDefault(kunde => kunde.Id == id);
            if (k == null)
            {
                throw new InvalidOperationException($"Kunde with id {id} is not in database");
            }
            return k;
        }

        public Kunde AddKunde(Kunde k)
        {
            k.Validate();
            Context.Add(k);
            Context.SaveChanges();
            return k;
        }

        public Kunde UpdateKunde(Kunde k)
        {
            k.Validate();
            var group = Context.Kunden.First(kunde => kunde.Id == k.Id);
            Context.Entry(group).CurrentValues.SetValues(k);
            Context.SaveChanges();
            return k;
        }

        public void SetKundeInactive(Kunde k)
        {
            k.IstAktiv = false;
            UpdateKunde(k);
        }

        public void DeleteKunde(Kunde k)
        {
            Context.Remove(k);
            Context.SaveChanges();
        }

        public List<Kunde> GetSearchResult(Kunde searchEntity)
        {
            if (searchEntity.Id != 0)
            {
                return new List<Kunde>
                {
                    GetKundeById(searchEntity.Id)
                };

            }

            List<Kunde> allKunden = GetKunden();
            IEnumerable<Kunde> searchResult = allKunden;
            PropertyInfo[] props = typeof(Kunde).GetProperties();

            foreach (var prop in props)
            {

                //id and istAktiv are not subject for searching -> these are the only ones with onn-string fields
                if (prop.PropertyType != typeof(string)) continue;


                string potentialSearchTerm = (string)prop.GetValue(searchEntity);
                if (potentialSearchTerm.HasSearchTerm())
                {
                    searchResult = searchResult.Where(k =>
                    {
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
