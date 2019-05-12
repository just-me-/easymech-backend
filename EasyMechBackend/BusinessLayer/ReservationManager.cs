using EasyMechBackend.DataAccessLayer;
using EasyMechBackend.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EasyMechBackend.Common.Exceptions;
using EasyMechBackend.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

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
            //Todo: Available, in besitz etc
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


        //probably not needed:
        //Todo: remove this if not needed
        public List<Reservation> GetSearchResult(Reservation searchEntity)
        {

            List<Reservation> allEntities = GetReservationen();
            IEnumerable<Reservation> searchResult = allEntities;
            PropertyInfo[] props = typeof(Reservation).GetProperties();

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

        //Get Rücknahme etc: Probably not needed. Here if we decided tgo use it anyway in some way:
        /*
         *TODO : Remove this if not needed
         *
         
        //Uebergabe und Ruecknahme
        public MaschinenUebergabe GetMaschinenUebergabe(long reservationsId)
        {
            Reservation r = Context.Reservationen
                .Include(res => res.Uebergabe)
                .SingleOrDefault(res => res.Id == reservationsId);

            if (r == null)
            {
                throw new ArgumentException($"Reservation {reservationsId} existiert nicht.");
            }
            return r.Uebergabe;
        }

        public MaschinenRuecknahme GetMaschinenRuecknahme(long reservationsId)
        {
            Reservation r = Context.Reservationen
                .Include(res => res.Ruecknahme)
                .SingleOrDefault(res => res.Id == reservationsId);

            if (r == null)
            {
                throw new ArgumentException($"Reservation {reservationsId} existiert nicht.");
            }
            return r.Ruecknahme;
        }


        public Reservation AddUebergabe(MaschinenUebergabe u)
        { 
            Context.Add(u);
            Context.SaveChanges();
            //Todo: Frage: Gut hier die Rservation zu returnen??? [bitte ja]
            return GetReseervationById(u.ReservationsId);
        }

        public Reservation AddRuecknahme(MaschinenRuecknahme u)
        {
            Context.Add(u);
            Context.SaveChanges();
            //Todo: Frage: Gut hier die Rservation zu returnen???
            return GetReseervationById(u.ReservationsId);
        }

        public Reservation UpdateUebergabe(MaschinenUebergabe u)
        {
            var entity = Context.MaschinenUebergaben.Single(ent => ent.Id == u.Id);
            Context.Entry(entity).CurrentValues.SetValues(u);
            Context.SaveChanges();

            //Todo: Frage: Gut hier die Rservation zu returnen???
            return GetReseervationById(entity.ReservationsId);
        }

        public Reservation UpdateRuecknahme(MaschinenRuecknahme u)
        {
            var entity = Context.MaschinenUebergaben.Single(ent => ent.Id == u.Id);
            Context.Entry(entity).CurrentValues.SetValues(u);
            Context.SaveChanges();
            //Todo: Frage: Gut hier die Rservation zu returnen???
            return GetReseervationById(u.ReservationsId);
        }

    */



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
            DateTime wantedEnd = r.Enddatum ?? new DateTime(2099,12,31);   //if there is no reservation end just reserve it "forever"
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
                    Bis = lv.Enddatum ?? new DateTime(2099, 12, 31)
                };

            foreach (var lv in reservedDates)
            {
                //ich reserviere, nachdem es jeman anders reserviert hat &&
                //der andere hat es noch nicht wieder zurückgegeben
                if (Helpers.Overlap(lv.Von, lv.Bis, wantedStart, wantedEnd))
                {
                    throw new ReservationException($"Die Maschine ist bereits vond Kunde {lv.KundenId} von {lv.Von.ToString("ddd dd.MM.yyyy")} bis {lv.Bis.ToString("ddd dd.MM.yyyy")} reserviert.");
                }

            }


        }

    }
}
