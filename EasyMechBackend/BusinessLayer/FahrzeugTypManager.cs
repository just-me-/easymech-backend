using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EasyMechBackend.DataAccessLayer;
using EasyMechBackend.Util;

namespace EasyMechBackend.BusinessLayer
{
    public class FahrzeugtypManager : ManagerBase
    {
        public FahrzeugtypManager(EMContext context)
        {
            Context = context;
        }

        public List<Fahrzeugtyp> GetFahrzeugtypen()
        {
            var query =
                from f in Context.Fahrzeugtypen
                orderby f.Id descending
                select f;
            return query.ToList();
        }

        public Fahrzeugtyp GetFahrzeugtypById(long id)
        {
            Fahrzeugtyp m = Context.Fahrzeugtypen.SingleOrDefault(fahrzeugtyp => fahrzeugtyp.Id == id);
            if (m == null)
            {
                throw new InvalidOperationException($"Fahrzeugtyp with id {id} is not in database");
            }
            return m;
        }

        public Fahrzeugtyp AddFahrzeugtyp(Fahrzeugtyp f)
        {
            f.Validate();
            Context.Add(f);
            Context.SaveChanges();
            return f;
        }

        public Fahrzeugtyp UpdateFahrzeugtyp(Fahrzeugtyp f)
        {
            f.Validate();
            var group = Context.Fahrzeugtypen.First(kunde => kunde.Id == f.Id);
            Context.Entry(group).CurrentValues.SetValues(f);
            Context.SaveChanges();
            return f;
        }

        public void DeleteFahrzeugtyp(Fahrzeugtyp f)
        {        
                Context.Remove(f);
                Context.SaveChanges();            
        }

        public List<Fahrzeugtyp> GetSearchResult(Fahrzeugtyp searchEntity)
        {
            if (searchEntity.Id != 0)
            {
                return new List<Fahrzeugtyp>
                {
                    GetFahrzeugtypById(searchEntity.Id)
                };
            }

            List<Fahrzeugtyp> allFahrzeugtypen = GetFahrzeugtypen();
            IEnumerable<Fahrzeugtyp> searchResult = allFahrzeugtypen;

            PropertyInfo[] props = typeof(Fahrzeugtyp).GetProperties();

            foreach (var prop in props)
            {
                //id and isActive are no subject for searching -> these are the only ones with onn-string fields
                if (prop.PropertyType != typeof(string)) continue;

                string potentialSearchTerm = (string)prop.GetValue(searchEntity);
                if (potentialSearchTerm.HasSearchTerm())
                {
                    searchResult = searchResult.Where(m => {
                        string contentOfCustomerThatIsEvaluated = (string)prop.GetValue(m);
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
                return new List<Fahrzeugtyp>();
            }
        }
    }
}
