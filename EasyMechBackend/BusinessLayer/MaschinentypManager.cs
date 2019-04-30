using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EasyMechBackend.Common.Exceptions;
using EasyMechBackend.DataAccessLayer;
using EasyMechBackend.Util;

namespace EasyMechBackend.BusinessLayer
{
    public class MaschinentypManager : ManagerBase
    {
        public MaschinentypManager(EMContext context)
        {
            Context = context;
        }

        public MaschinentypManager()
        {
            Context = new EMContext();
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
            else {
            Context.Remove(f);
            Context.SaveChanges();
            }
        }
        
        
    }
}
