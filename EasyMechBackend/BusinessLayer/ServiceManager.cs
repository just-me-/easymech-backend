using EasyMechBackend.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using EasyMechBackend.DataAccessLayer.Entities;
using static EasyMechBackend.Common.EnumHelper;
using Microsoft.EntityFrameworkCore;
using EasyMechBackend.Common.DataTransferObject;

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

        public List<Service> GetServices(ServiceState status)
        {
            var query =
            from r in Context.Services
            .Include(a => a.Arbeitsschritte)
            .Include(m => m.Materialposten)
            where status == ServiceState.All || r.Status == status
            orderby r.Beginn descending
            select r;
            return query.ToList();
        }

        public Service GetServiceById(long id)
        {
            Service r = Context.Services
                .Include(a => a.Arbeitsschritte)
                .Include(m => m.Materialposten)
                .SingleOrDefault(res => res.Id == id);
            if (r == null)
            {
                throw new InvalidOperationException($"Service with id {id} is not in database");
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
            s.Validate();
            var old = Context.Services
                .Single(res => res.Id == s.Id);
            foreach (var schritt in old.Arbeitsschritte)
            {
                Context.Remove(schritt);
            }
            foreach (var material in old.Materialposten)
            {
                Context.Remove(material);
            }
            Context.SaveChanges();
            Context.Entry(old).State = EntityState.Detached;

            Context.Entry(old).CurrentValues.SetValues(s);
            Context.SaveChanges();
            return s;
        }

        public void DeleteService(Service s)
        {
            foreach (Materialposten m in s.Materialposten)
            {
                Context.Remove(m);
            }
            foreach (Arbeitsschritt a in s.Arbeitsschritte)
            {
                Context.Remove(a);
            }
            Context.Remove(s);
            Context.SaveChanges();
        }

        public List<Service> GetServiceSearchResult(ServiceSearchDto searchEntity)
        {
            var query = from t in Context.Services
                    .Include(ser => ser.Arbeitsschritte)
                    .Include(ser => ser.Materialposten)
                    .Include(m => m.Maschine)
                        where searchEntity.KundenId == null || searchEntity.KundenId == t.KundenId
                        where searchEntity.MaschinenId == null || searchEntity.MaschinenId == t.MaschinenId
                        where searchEntity.MaschinentypId == null || searchEntity.MaschinentypId == t.Maschine.MaschinentypId
                        where searchEntity.Von == null || searchEntity.Von <= t.Beginn
                        where searchEntity.Bis == null || t.Ende <= searchEntity.Bis
                        where searchEntity.Status == null || searchEntity.Status == 0 || searchEntity.Status == t.Status
                        select t;

            return query.OrderByDescending(t => t.Beginn).ToList();
        }
    }
}
