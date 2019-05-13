using EasyMechBackend.DataAccessLayer;
using EasyMechBackend.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EasyMechBackend.DataAccessLayer.Entities;

namespace EasyMechBackend.BusinessLayer
{
    public class GeplanterServiceManager : ManagerBase
    {

        public GeplanterServiceManager(EMContext context) : base(context)
        {
        }


        public GeplanterServiceManager()
        {
        }


        public List<GeplanterService> GetGeplanteServices()
        {
            var query =
            from r in Context.GeplanteServices
            orderby r.Beginn descending
            select r;
            return query.ToList();
        }

        public GeplanterService GetGeplanterServiceById(long id)
        {
            GeplanterService r = Context.GeplanteServices.SingleOrDefault(res => res.Id == id);
            if (r == null)
            {
                throw new InvalidOperationException($"GeplanterService with id {id} is not in database");
            }
            return r;
        }

        public GeplanterService AddGeplanterService(GeplanterService s)
        {
            s.Validate();
            Context.Add(s);
            Context.SaveChanges();
            return s;
        }

        public GeplanterService UpdateGeplanterService(GeplanterService s)
        {
            s.Validate();
            var entity = Context.GeplanteServices.Single(res => res.Id == s.Id);
            Context.Entry(entity).CurrentValues.SetValues(s);
            Context.SaveChanges();
            return entity;
        }


        public void DeleteGeplanterService(GeplanterService s)
        {
            Context.Remove(s);
            Context.SaveChanges();
        }

        public List<GeplanterService> GetSearchResult(GeplanterService searchEntity)
        {

            List<GeplanterService> allEntities = GetGeplanteServices();
            IEnumerable<GeplanterService> searchResult = allEntities;
            PropertyInfo[] props = typeof(GeplanterService).GetProperties();

            foreach (var prop in props)
            {
                // Handling String Fields with lower case contains
                if (prop.PropertyType == typeof(string))
                {
                    string potentialSearchTerm = (string) prop.GetValue(searchEntity);
                    if (potentialSearchTerm.HasSearchTerm())
                    {
                        searchResult = searchResult.Where(m =>
                        {
                            string contentOfEntityThatIsEvaluated = (string) prop.GetValue(m);
                            return contentOfEntityThatIsEvaluated != null &&
                                   contentOfEntityThatIsEvaluated.ContainsCaseInsensitive(potentialSearchTerm);
                        });
                    }
                }

                // Handling int or int? Fields with exact match
                else if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
                {
                    int targetValue = (int?) prop.GetValue(searchEntity) ?? 0;
                    if (targetValue != 0)
                    {
                        searchResult = searchResult.Where(m =>
                        {
                            int contentOfEntityThatIsEvaluated = (int?) prop.GetValue(m) ?? 0;
                            return contentOfEntityThatIsEvaluated == targetValue;
                        });
                    }
                }

                //Handling long (PK, FK) with exact matching
                //seperate treatment to int is necessary as int can't be castet to long?
                else if (prop.PropertyType == typeof(long) || prop.PropertyType == typeof(long?))
                {
                    long targetValue = (long?) prop.GetValue(searchEntity) ?? 0;
                    if (targetValue != 0)
                    {
                        searchResult = searchResult.Where(m =>
                        {
                            long contentOfEntityThatIsEvaluated = (long?) prop.GetValue(m) ?? 0;
                            return contentOfEntityThatIsEvaluated == targetValue;
                        });
                    }
                }

                //Handling DateTime Fields with exact match
                else if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?))
                {
                    DateTime? targetValueOrNull = (DateTime?) prop.GetValue(searchEntity);
                    if (targetValueOrNull == null) continue;

                    DateTime targetValue = (DateTime) targetValueOrNull;
                    searchResult = searchResult.Where(m =>
                    {
                        DateTime? contentOfEntityThatIsEvaluated = (DateTime?) prop.GetValue(m);
                        return contentOfEntityThatIsEvaluated != null &&
                               DateTime.Equals((DateTime) contentOfEntityThatIsEvaluated, targetValue);
                    });
                }
            }

            return searchResult.ToList();
        } 
    }
}
