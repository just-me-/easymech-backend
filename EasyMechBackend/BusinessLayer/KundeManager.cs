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
        
        public KundeManager(EMContext context)
        {
            Context = context;
        }

        public KundeManager()
        {
            Context = new EMContext();
        }



        public List<Kunde> GetKunden(bool withInactive)
        {
            var query =
            from k in Context.Kunden
            where k.IstAktiv.Value || withInactive
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

            List<Kunde> allKunden = GetKunden(false);
            IEnumerable<Kunde> searchResult = allKunden;
            PropertyInfo[] props = typeof(Kunde).GetProperties();

            foreach (var prop in props)
            {

                //id and istAktiv are not subject for searching -> these are the only ones with non-string fields
                //no other fields than string fields have to be treated.
                if (prop.PropertyType != typeof(string)) continue;


                string potentialSearchTerm = (string)prop.GetValue(searchEntity);
                if (potentialSearchTerm.HasSearchTerm())
                {
                    searchResult = searchResult.Where(k =>
                    {
                        string contentOfCustomerThatIsEvaluated = (string)prop.GetValue(k);
                        return contentOfCustomerThatIsEvaluated != null &&
                               contentOfCustomerThatIsEvaluated.ContainsCaseInsensitive(potentialSearchTerm);

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
