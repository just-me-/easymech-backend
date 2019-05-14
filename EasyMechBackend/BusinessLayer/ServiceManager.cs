using EasyMechBackend.DataAccessLayer;
using EasyMechBackend.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EasyMechBackend.DataAccessLayer.Entities;

namespace EasyMechBackend.BusinessLayer
{
    public class ServiceManager : ManagerBase
    {

        public ServiceManager(EMContext context) : base(context)
        {
        }

        public ServiceManager()
        {
        }
        public List<Service> GetAllServices()
        {
            var query =
            from r in Context.Services
            orderby r.Beginn descending
            select r;
            return query.ToList();
        }

        public List<Service> GetServices(Service.ServiceStatus status)
        {
            var query =
            from r in Context.Services
            where r.Status == status
            orderby r.Beginn descending
            select r;
            return query.ToList();
        }
        
        public Service GetServiceById(long id)
        {
            Service r = Context.Services.SingleOrDefault(res => res.Id == id);
            if (r == null)
            {
                throw new InvalidOperationException($"GeplanterService with id {id} is not in database");
            }
            return r;
        }

        public Service AddService(Service s)
        {
            s.Validate();
            Context.Add(s);
            Context.SaveChanges();
            return s;
        }

        public Service UpdateService(Service s)
        {
            //Todo: update arbeitsschritte und material
            s.Validate();
            var entity = Context.Services.Single(res => res.Id == s.Id);
            Context.Entry(entity).CurrentValues.SetValues(s);
            Context.SaveChanges();
            return entity;
        }


        public void DeleteService(Service s)
        {
            foreach(Materialposten m in s.Materialposten) {
                Context.Remove(m);
            }
            foreach(Arbeitsschritt a in s.Arbeitsschritte)
            {
                Context.Remove(a);
            }
            Context.Remove(s);
            Context.SaveChanges();
        }

        public List<Service> GetSearchResult(Service searchEntity)
        {

            List<Service> allEntities = GetAllServices();
            IEnumerable<Service> searchResult = allEntities;
            PropertyInfo[] props = typeof(Service).GetProperties();

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
