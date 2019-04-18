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
        private EMContext _context;

        public MaschineManager(EMContext context)
        {
            _context = context;
        }

        public List<Maschine> GetMaschinen()
        {
            var query =
                from k in _context.Maschinen
                where k.IsActive.Value
                orderby k.Id descending
                select k;
            return query.ToList();
        }

        public Maschine GetMaschineById(long id)
        {
            Maschine m = _context.Maschinen.SingleOrDefault(maschine => maschine.Id == id);
            if (m == null)
            {
                throw new InvalidOperationException($"Kunde with id {id} is not in database");
            }
            return m;
        }

        public Maschine AddMaschine(Maschine m)
        {
            m.Validate();
            _context.Add(m);
            _context.SaveChanges();
            return m;
        }

        public Maschine UpdateMaschine(Maschine m)
        {
            m.Validate();
            _context.Entry(m).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return m;
        }

        public void SetMaschineInactive(Maschine m)
        {
                m.IsActive = false;
                UpdateMaschine(m);            
        }

        public void DeleteMaschine(Maschine m)
        {        
                _context.Remove(m);
                _context.SaveChanges();            
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
                    searchResult = searchResult.Where(k => {
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
                return new List<Maschine>();
            }

        }
    }
}
