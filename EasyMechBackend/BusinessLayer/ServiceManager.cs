using EasyMechBackend.DataAccessLayer;
using EasyMechBackend.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using EasyMechBackend.DataAccessLayer.Entities;
using static EasyMechBackend.Common.EnumHelper;
using Microsoft.EntityFrameworkCore;
using EasyMechBackend.Common.DataTransferObject;
using EasyMechBackend.Common.Exceptions;

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
            foreach (var service in query)
            {
                SortArbeitsschritteAndMaterial(service);
            }
            return query.ToList();
        }

        public Service GetServiceById(long id)
        {
            Service s = Context.Services
                .Include(a => a.Arbeitsschritte)
                .Include(m => m.Materialposten)
                .SingleOrDefault(res => res.Id == id);
            if (s == null)
            {
                throw new InvalidOperationException($"Service with id {id} is not in database");
            }
            SortArbeitsschritteAndMaterial(s);
            return s;
        }

        public Service AddService(Service s)
        {
            CheckAndValidate(s);
            Context.Add(s);
            Context.SaveChanges();
            SortArbeitsschritteAndMaterial(s);
            return s;
        }

        public Service UpdateService(Service s)
        {
            CheckAndValidate(s);
            var old = Context.Services
                .Single(res => res.Id == s.Id);
            if (old.Arbeitsschritte != null)
            {
                foreach (var schritt in old.Arbeitsschritte)
                {
                    Context.Remove(schritt);
                }
            }

            if (old.Materialposten != null)
            {
                foreach (var material in old.Materialposten)
                {
                    Context.Remove(material);
                }
            }

            Context.SaveChanges();
            Context.Entry(old).State = EntityState.Detached;

            Context.Update(s);
            Context.SaveChanges();
            SortArbeitsschritteAndMaterial(s);
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
            foreach (var service in query)
            {
                SortArbeitsschritteAndMaterial(service);
            }
            return query.OrderByDescending(t => t.Beginn).ToList();
        }


        private void CheckAndValidate(Service s)
        { 
            EnsureStartBeforeEnd(s);
            EnsureNoOverlappingMaintenances(s);
            EnsureNoOverlappingReservations(s);
            s.Validate();
        }

        private static void EnsureStartBeforeEnd(Service s)
        {
            var start = s.Beginn;
            var end = s.Ende;
            if (start > end)
            {
                throw new MaintenanceException("Das Startdatum liegt vor dem Enddatum.");
            }
        }

        private void EnsureNoOverlappingMaintenances(Service s)
        {
            DateTime wantedStart = s.Beginn;
            DateTime wantedEnd = s.Ende;
            var myAuto = s.MaschinenId;
            var myService = s.Id;

            var serviceDates = from lv in Context.Services
                                where lv.MaschinenId == myAuto && lv.Id != myService
                                where Helpers.Overlap(lv.Beginn,
                                    lv.Ende,
                                    wantedStart,
                                    wantedEnd)
                                select new
                                {
                                    Von = lv.Beginn,
                                    Bis = lv.Ende 
                                };

            if (!serviceDates.Any()) return;
            var overlappingEntity = serviceDates.First();
            string msg =
                $"Die Maschine befindet sich bereits von {overlappingEntity.Von:ddd dd.MM.yyyy} " +
                $"bis {overlappingEntity.Bis:ddd dd.MM.yyyy} im Service";
            throw new MaintenanceException(msg);

        }

        private void EnsureNoOverlappingReservations(Service r)
        {

            DateTime wantedStart = r.Beginn;
            DateTime wantedEnd = r.Ende;
            var myAuto = r.MaschinenId;

            var reservedDates =
                from lv in Context.Reservationen.Include(reser => reser.Kunde)
                where lv.MaschinenId == myAuto
                where Helpers.Overlap(lv.Startdatum ?? DateTime.Now,
                    lv.Enddatum ?? DateTime.MaxValue,
                    wantedStart,
                    wantedEnd)
                select new
                {
                    Kunde = lv.Kunde.Firma,
                    Von = lv.Startdatum ?? DateTime.Now,
                    Bis = lv.Enddatum ?? DateTime.MaxValue
                };

            if (!reservedDates.Any()) return;
            var overlappingEntity = reservedDates.First();
            string msg = $"Die Maschine ist vom {overlappingEntity.Von:ddd dd.MM.yyyy} " +
                         $"bis {overlappingEntity.Bis:ddd dd.MM.yyyy} " +
                         $"von {overlappingEntity.Kunde} reserviert und nicht für einen Service verfügbar.";
            throw new MaintenanceException(msg);

        }

        private static void SortArbeitsschritteAndMaterial(Service s)
        {
            s.Arbeitsschritte = s.Arbeitsschritte.OrderBy(a => a.Id).ToList();
            s.Materialposten = s.Materialposten.OrderBy(m => m.Id).ToList();
        }
    }
}
