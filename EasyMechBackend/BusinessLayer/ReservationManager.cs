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
            from r in Context.Reservationen
            orderby r.Startdatum descending
            select r;
            return query.ToList();
        }

        public Reservation GetReseervationById(long id)
        {
            Reservation r = Context.Reservationen.SingleOrDefault(res => res.Id == id);
            if (r == null)
            {
                throw new InvalidOperationException($"Reservation with id {id} is not in database");
            }
            return r;
        }

        public Reservation AddReservation(Reservation r)
        {
            //Todo: Available, in besitz etc
            r.Validate();
            Context.Add(r);
            Context.SaveChanges();
            return r;
        }

        public Reservation UpdateReservation(Reservation r)
        {
            //Todo: Available, in besitz etc
            r.Validate();
            var entity = Context.Reservationen.Single(res => res.Id == r.Id);
            Context.Entry(entity).CurrentValues.SetValues(r);
            Context.SaveChanges();
            return entity;
        }


        public void DeleteReservation(Reservation r)
        {
            Context.Remove(r);
            Context.SaveChanges();
        }

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

    }
}
