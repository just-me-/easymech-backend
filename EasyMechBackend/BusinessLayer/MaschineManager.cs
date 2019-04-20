using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EasyMechBackend.DataAccessLayer;
using EasyMechBackend.Util;

namespace EasyMechBackend.BusinessLayer
{
    public class MaschineManager : ManagerBase
    {
        public EMContext Context { get; set; }

        public MaschineManager(EMContext context)
        {
            Context = context;

            if(!GetMaschinen().Any())
            {
                Maschine m1 = new Maschine
                {
                    Seriennummer = "111"
                };
                AddMaschine(m1);
            }
        }

        public List<Maschine> GetMaschinen()
        {
            var query =
                from m in Context.Maschinen
                where m.IsActive.Value
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
            Context.Add(m);
            Context.SaveChanges();
            return m;
        }

        public Maschine UpdateMaschine(Maschine m)
        {
            m.Validate();
            Context.Entry(m).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Context.SaveChanges();
            return m;
        }

        public void SetMaschineInactive(Maschine m)
        {
                m.IsActive = false;
                UpdateMaschine(m);            
        }

        public void DeleteMaschine(Maschine m)
        {        
                Context.Remove(m);
                Context.SaveChanges();            
        }

        public List<Maschine> GetSearchResult(Maschine searchEntity)
        {
            if (searchEntity.Id != 0)
            {
                return new List<Maschine>
                {
                    GetMaschineById(searchEntity.Id)
                };
            }

            List<Maschine> allMaschinen = GetMaschinen();
            IEnumerable<Maschine> searchResult = allMaschinen;

            PropertyInfo[] props = typeof(Maschine).GetProperties();

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
                return new List<Maschine>();
            }

        }
    }
}
