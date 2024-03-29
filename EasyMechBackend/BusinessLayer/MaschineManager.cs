﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EasyMechBackend.Common.Exceptions;
using EasyMechBackend.DataAccessLayer;
using EasyMechBackend.DataAccessLayer.Entities;
using EasyMechBackend.Util;

namespace EasyMechBackend.BusinessLayer
{
    public class MaschineManager : ManagerBase
    {
        public MaschineManager(EMContext context) : base(context)
        {
        }

        public MaschineManager()
        {
        }

        public List<Maschine> GetMaschinen(bool withInactive)
        {
            var query =
                from m in Context.Maschinen
                where m.IstAktiv.Value || withInactive
                orderby m.Id descending
                select m;
            return query.ToList();
        }

        public Maschine GetMaschineById(long id)
        {
            Maschine m = Context.Maschinen.SingleOrDefault(maschine => maschine.Id == id);
            if (m == null)
            {
                throw new InvalidOperationException($"Maschine with id {id} is not in database");
            }
            return m;
        }

        public Maschine AddMaschine(Maschine m)
        {
            m.Validate();
            if (IsRestoreOperation(m))
            {
                long id = Context.Maschinen.First(c => c.Seriennummer == m.Seriennummer).Id;
                m.Id = id;
                m.IstAktiv = true;
                return UpdateMaschine(m);
            }
            EnsureUniqueness(m);
            Context.Add(m);
            Context.SaveChanges();
            return m;
        }

        public Maschine UpdateMaschine(Maschine m)
        {
            m.Validate();
            EnsureUniqueness(m);
            var group = Context.Maschinen.First(kunde => kunde.Id == m.Id);
            Context.Entry(group).CurrentValues.SetValues(m);
            Context.SaveChanges();
            return m;
        }

        public void SetMaschineInactive(Maschine m)
        {
                m.IstAktiv = false;
                UpdateMaschine(m);            
        }

        public void DeleteMaschine(Maschine m)
        {        
                Context.Remove(m);
                Context.SaveChanges();            
        }

        public List<Maschine> GetSearchResult(Maschine searchEntity)
        {

            List<Maschine> allMaschinen = GetMaschinen(false);
            IEnumerable<Maschine> searchResult = allMaschinen;

            PropertyInfo[] props = typeof(Maschine).GetProperties();

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

        private void EnsureUniqueness(Maschine m)
        {
            var query = from laufvar in Context.Maschinen
                        where laufvar.Seriennummer == m.Seriennummer
                        where laufvar.Seriennummer != null 
                        where laufvar.Id != m.Id
                        where (laufvar.IstAktiv ?? false)
                        select 0;

            if (query.Any())
            {
                throw new UniquenessException($"Die Maschinen-Seriennummer {m.Seriennummer} ist bereits im System registriert.");
            }
        }

        private bool IsRestoreOperation(Maschine m)
        {
            var query = from laufvar in Context.Maschinen
                where laufvar.Seriennummer == m.Seriennummer
                where !(laufvar.IstAktiv ?? true)
                select m;
            return query.Any();
        }
    }
}
