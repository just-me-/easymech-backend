using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EasyMechBackend.Common.Exceptions;
using EasyMechBackend.DataAccessLayer;
using EasyMechBackend.DataAccessLayer.Entities;
using EasyMechBackend.Util;

namespace EasyMechBackend.BusinessLayer
{
    public class MaschinentypManager : ManagerBase
    {
        public MaschinentypManager(EMContext context) : base(context)
        {
        }

        public MaschinentypManager()
        {
        }

        public List<Maschinentyp> GetMaschinentypen()
        {
            var query =
                from f in Context.Maschinentypen
                orderby f.Id descending
                select f;
            return query.ToList();
        }

        public Maschinentyp GetMaschinentypById(long id)
        {
            Maschinentyp m = Context.Maschinentypen.SingleOrDefault(maschinentyp => maschinentyp.Id == id);
            if (m == null)
            {
                throw new InvalidOperationException($"Maschinentyp with id {id} is not in database");
            }
            return m;
        }

        public Maschinentyp AddMaschinentyp(Maschinentyp f)
        {
            f.Validate();
            Context.Add(f);
            Context.SaveChanges();
            return f;
        }

        public Maschinentyp UpdateMaschinentyp(Maschinentyp f)
        {
            f.Validate();
            var group = Context.Maschinentypen.First(kunde => kunde.Id == f.Id);
            Context.Entry(group).CurrentValues.SetValues(f);
            Context.SaveChanges();
            return f;
        }

        public void DeleteMaschinentyp(Maschinentyp f)
        {

            var query =
                from m in Context.Maschinen
                where m.MaschinentypId == f.Id
                select m;

            bool restricted = query.Any();

            if (restricted)
            {
                throw new ForeignKeyRestrictionException($"Error: Maschinentyp {f.Id} ({f.Fabrikat}) is still set as other machine's type and can't be deleted!");
            }
            else
            {
                Context.Remove(f);
                Context.SaveChanges();
            }
        }

        public List<Maschinentyp> GetSearchResult(Maschinentyp searchEntity)
        {
            if (searchEntity.Id != 0)
            {
                return new List<Maschinentyp>
                {
                    GetMaschinentypById(searchEntity.Id)
                };
            }

            List<Maschinentyp> allMaschinentypen = GetMaschinentypen();
            IEnumerable<Maschinentyp> searchResult = allMaschinentypen;

            PropertyInfo[] props = typeof(Maschinentyp).GetProperties();

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

    }
}
