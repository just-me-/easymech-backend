using EasyMechBackend.DataAccessLayer;
using EasyMechBackend.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EasyMechBackend.Common.Exceptions;
using EasyMechBackend.DataAccessLayer.Entities;

namespace EasyMechBackend.BusinessLayer
{
    public class KundeManager : ManagerBase
    {

        public KundeManager(EMContext context) : base(context)
        {
        }


        public KundeManager()
        {
        }


        public List<Kunde> GetKunden(bool withInactive)
        {
            var query =
            from k in Context.Kunden
            where k.IstAktiv.Value || withInactive
            where k.Id != 1
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
            EnsureUniqueness(k);
            Context.Add(k);
            Context.SaveChanges();
            return k;
        }

        public Kunde UpdateKunde(Kunde k)
        {
            k.Validate();
            EnsureUniqueness(k);
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

            List<Kunde> allKunden = GetKunden(false);
            IEnumerable<Kunde> searchResult = allKunden;
            PropertyInfo[] props = typeof(Kunde).GetProperties();

            foreach (var prop in props)
            {
                // Handling String Fields with lower case contains
                if (prop.PropertyType == typeof(string))
                {
                    string potentialSearchTerm = (string)prop.GetValue(searchEntity);
                    if (potentialSearchTerm.HasSearchTerm())
                    {
                        searchResult = searchResult.Where(m =>
                        {
                            string contentOfEntityThatIsEvaluated = (string)prop.GetValue(m);
                            return contentOfEntityThatIsEvaluated != null &&
                                   contentOfEntityThatIsEvaluated.ContainsCaseInsensitive(potentialSearchTerm);
                        });
                    }
                }

                // Handling int or int? Fields with exact match
                else if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
                {
                    int targetValue = (int?)prop.GetValue(searchEntity) ?? 0;
                    if (targetValue != 0)
                    {
                        searchResult = searchResult.Where(m =>
                        {
                            int contentOfEntityThatIsEvaluated = (int?)prop.GetValue(m) ?? 0;
                            return contentOfEntityThatIsEvaluated == targetValue;
                        });
                    }
                }

                //Handling long (PK, FK) with exact matching
                //seperate treatment to int is necessary as int can't be castet to long?
                else if (prop.PropertyType == typeof(long) || prop.PropertyType == typeof(long?))
                {
                    long targetValue = (long?)prop.GetValue(searchEntity) ?? 0;
                    if (targetValue != 0)
                    {
                        searchResult = searchResult.Where(m =>
                        {
                            long contentOfEntityThatIsEvaluated = (long?)prop.GetValue(m) ?? 0;
                            return contentOfEntityThatIsEvaluated == targetValue;
                        });
                    }
                }
            }

            return searchResult.ToList();

        }


        private void EnsureUniqueness(Kunde k)
        {
            var query = from laufvar in Context.Kunden
                where laufvar.Firma == k.Firma 
                where laufvar.Firma != null 
                where laufvar.Id != k.Id
                where  (laufvar.IstAktiv ?? false)
                select 0;
            if (query.Any())
            {
                throw new UniquenessException($"Die Firma \"{k.Firma}\" ist bereits im System registriert.");
            }
        }
    }
}
