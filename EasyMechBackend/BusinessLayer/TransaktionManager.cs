using EasyMechBackend.DataAccessLayer;
using EasyMechBackend.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EasyMechBackend.DataAccessLayer.Entities;

namespace EasyMechBackend.BusinessLayer
{
    public class TransaktionManager : ManagerBase
    {
        public TransaktionManager(EMContext context) : base(context)
        {
        }

        public TransaktionManager()
        {
        }

        public List<Transaktion> GetTransaktionen()
        {
            var query =
            from t in Context.Transaktionen
            orderby t.Id descending
            select t;
            return query.ToList();
        }

        public List<Transaktion> GetVerkaeufe()
        {
            var query =
            from t in Context.Transaktionen
            where t.Typ == Transaktion.TransaktionsTyp.Verkauf
            orderby t.Id descending
            select t;
            return query.ToList();
        }

        public List<Transaktion> GetEinkaeufe()
        {
            var query =
            from t in Context.Transaktionen
            where t.Typ == Transaktion.TransaktionsTyp.Einkauf
            orderby t.Id descending
            select t;
            return query.ToList();
        }

        public Transaktion GetTransaktionById(long id)
        {
            Transaktion k = Context.Transaktionen.SingleOrDefault(transaktion => transaktion.Id == id);
            if (k == null)
            {
                throw new InvalidOperationException($"Transaktion with id {id} is not in database");
            }
            return k;
        }

        public Transaktion AddTransaktion(Transaktion t)
        {
            Context.Add(t);
            Context.SaveChanges();
            return t;
        }

        public Transaktion UpdateTransaktion(Transaktion k)
        {
            var group = Context.Transaktionen.First(transaktion => transaktion.Id == k.Id);
            Context.Entry(group).CurrentValues.SetValues(k);
            Context.SaveChanges();
            return k;
        }

        public void DeleteTransaktion(Transaktion k)
        {
            Context.Remove(k);
            Context.SaveChanges();
        }

        public List<Transaktion> GetSearchResult(Transaktion searchEntity)
        {
            if (searchEntity.Id != 0)
            {
                return new List<Transaktion>
                {
                    GetTransaktionById(searchEntity.Id)
                };

            }

            List<Transaktion> allTransaktionen = GetTransaktionen();
            IEnumerable<Transaktion> searchResult = allTransaktionen;
            PropertyInfo[] props = typeof(Transaktion).GetProperties();

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

            return searchResult.ToList();
        }
    }
}
