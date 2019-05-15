using EasyMechBackend.DataAccessLayer;
using EasyMechBackend.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EasyMechBackend.Common.DataTransferObject;
using EasyMechBackend.Common.Exceptions;
using EasyMechBackend.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using EasyMechBackend.Common;
using static EasyMechBackend.Common.EnumHelper;

namespace EasyMechBackend.BusinessLayer
{
    public class ReservationManager : ManagerBase
    {

        public ReservationManager(EMContext context) : base(context)
        {
        }

        public ReservationManager()
        {
        }

        public List<Reservation> GetReservationen()
        {
            var query =
            from r in Context.Reservationen.Include(a => a.Uebergabe).Include(a => a.Ruecknahme)
            orderby r.Startdatum descending
            select r;
            return query.ToList();
        }

        public Reservation GetReseervationById(long id)
        {
            Reservation r = Context.Reservationen.Include(a => a.Uebergabe).Include(a => a.Ruecknahme).SingleOrDefault(res => res.Id == id);
            if (r == null)
            {
                throw new InvalidOperationException($"Reservation with id {id} is not in database");
            }
            return r;
        }

        public Reservation AddReservation(Reservation r)
        {

            CheckAndValidate(r);


            Context.Add(r);
            Context.SaveChanges();
            return r;
        }

        public Reservation UpdateReservation(Reservation r)
        {
            //Comment on WHY [=comment allowed] the following code is as it is:
            //I delete the old übergabe und rücknahme, and than i just add new ones (or leave them as null if wanted so).
            //This way it does not matter, if the rücknahme/übergabe-operation is add, delete, or update.
            //We also dont have to care about a ReservationsId-FK-must-be-unique-in-Uebergabe-Restriction, as the old entity is deleted and the FK will be available again.
            //otherwise we would have to care if we actually add or update the übergabe.
            //and, as a bonus, the frontend is totally absolutely independent of the Fields "Id" and "ReservationsId" of a Übergabe/Rücknahme.
            //So much independent that the DTO does actually not even contain these fields.

            CheckAndValidate(r);

            Reservation old = Context.Reservationen
                .Include(res => res.Uebergabe)
                .Include(res => res.Ruecknahme)
                .Single(res => res.Id == r.Id);

            if (old.Uebergabe != null)
            {
                Context.Remove(old.Uebergabe);
            }

            if (old.Ruecknahme != null)
            {
                Context.Remove(old.Ruecknahme);
            }

            Context.SaveChanges();
            Context.Entry(old).State = EntityState.Detached;

            
            Context.Update(r);
            Context.SaveChanges();
            return r;
        }


        public void DeleteReservation(Reservation r)
        {
            Context.Remove(r);       //cascade deletes the übergabe rücknahme
            Context.SaveChanges();
        }


        public List<Reservation> GetServiceSearchResult(ServiceSearchDto searchEntity)
        {

            var query = from t in Context.Reservationen
                    .Include(res => res.Uebergabe)
                    .Include(res => res.Ruecknahme)
                where searchEntity.KundenId == null || searchEntity.KundenId == t.KundenId
                where searchEntity.MaschinenId == null || searchEntity.MaschinenId == t.MaschinenId
                where searchEntity.MaschinentypId == null || searchEntity.MaschinentypId == t.Maschine.MaschinentypId
                where searchEntity.Von == null || searchEntity.Von <= t.Startdatum
                where searchEntity.Bis == null || t.Enddatum <= searchEntity.Bis
                select t;

            switch (searchEntity.Status)
            {
                case ServiceState.All:
                case null:
                    break;
                case ServiceState.Pending:
                    query = query.Where(t => t.Uebergabe == null);
                    break;
                case ServiceState.Running:
                    query = query.Where(t => t.Uebergabe != null && t.Ruecknahme == null);
                    break;
                case ServiceState.Completed:
                    query = query.Where(t => t.Ruecknahme != null); //note: übergabe can't be null if rücknahme is set. class invariant.
                    break;
                default:
                    throw new ArgumentException("Konnte den Service-Status nicht einordnen.");
            }

            return query.OrderByDescending(t => t.Startdatum).ToList();
        }
        
        //TODO: Methoden in statische klasse auslagern, beiirgendwo inner dates iwas noch context migeben halt.

        private void CheckAndValidate(Reservation r)
        {
            CopyInnerDates(r);                  //if pickup or return exists, take those inner dates as reservation dates.
            EnsureOwnProperty(r);               //checks if vehicle has owner 1
            EnsureReturnHasPrecedingPickup(r);  //no return if there was no pickup before
            EnsureStartBeforeEnd(r);            //checks if start < enddate
            EnsureNoOverlappingReservations(r); //checks ifr already reserved
            r.Validate();                       //clips properties
        }

        private void EnsureOwnProperty(Reservation r)
        {
            Maschine ma = Context.Maschinen.SingleOrDefault(m => m.Id == r.MaschinenId);

            if (ma.BesitzerId != 1)
            {
                throw new ReservationException($"Maschine {r.MaschinenId} befindet sich nicht in Eigenbesitz. (Besitzer Id: {ma.BesitzerId})");
            }
        }

        private void CopyInnerDates(Reservation r)
        {
            if (r.Uebergabe != null)
            {
                r.Startdatum = r.Uebergabe.Datum;
            }
            if (r.Ruecknahme != null)
            {
                r.Enddatum = r.Ruecknahme.Datum;
            }
        }

        private void EnsureReturnHasPrecedingPickup(Reservation r)
        {
            if (r.Uebergabe == null && r.Ruecknahme != null)
            {
                throw new ReservationException("Sie können keine Rückgabe erfassen, wenn es keine Übergabe gibt.");
            }
        }

        private void EnsureStartBeforeEnd(Reservation r)
        {
            var start = r.Startdatum;
            var end = r.Enddatum;
            if (start > end)
            {
                throw new ReservationException("Das Reservations-Startdatum liegt vor dem Enddatum, bzw die Rückgabe ist vor der Abholung.");
            }
        }

        private void EnsureNoOverlappingReservations(Reservation r)
        {

            DateTime wantedStart = r.Startdatum ?? DateTime.Now;
            DateTime wantedEnd = r.Enddatum ?? DateTime.MaxValue;   //if there is no reservation end just reserve it "forever"
            var myAuto = r.MaschinenId;
            var myReservation = r.Id;

            List<Reservation> reservationes = GetReservationen();

            var reservedDates =
                from lv in reservationes
                where lv.MaschinenId == myAuto && lv.Id != myReservation
                select new
                {
                    lv.KundenId,
                    Von = lv.Startdatum ?? DateTime.Now,
                    Bis = lv.Enddatum ?? DateTime.MaxValue
        };

            foreach (var lv in reservedDates)
            {
                //ich reserviere, nachdem es jeman anders reserviert hat &&
                //der andere hat es noch nicht wieder zurückgegeben
                if (Helpers.Overlap(lv.Von, lv.Bis, wantedStart, wantedEnd))
                {

                    //TOdo: Message verschönern:
                    //Wer ist Kunde 5? "Bis 12.12.9999 reserviert" <=> "open-end reserviert"
                    throw new ReservationException($"Die Maschine ist bereits vond Kunde {lv.KundenId} von {lv.Von.ToString("ddd dd.MM.yyyy")} bis {lv.Bis.ToString("ddd dd.MM.yyyy")} reserviert.");
                }

            }


        }

    }
}
