using EasyMechBackend.DataAccessLayer;
using EasyMechBackend.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EasyMechBackend.Common.DataTransferObject;
using EasyMechBackend.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Clauses;

namespace EasyMechBackend.BusinessLayer
{
    public class TransaktionManager : ManagerBase
    {
        public TransaktionManager(EMContext context) : base(context)
        {
        }

        public TransaktionManager()
        {
        }

        public List<Transaktion> GetTransaktionen()
        {
            var query =
            from t in Context.Transaktionen
            orderby t.Id descending
            select t;
            return query.ToList();
        }

        public List<Transaktion> GetVerkaeufe()
        {
            var query =
            from t in Context.Transaktionen
            where t.Typ == Transaktion.TransaktionsTyp.Verkauf
            orderby t.Id descending
            select t;
            return query.ToList();
        }

        public List<Transaktion> GetEinkaeufe()
        {
            var query =
            from t in Context.Transaktionen
            where t.Typ == Transaktion.TransaktionsTyp.Einkauf
            orderby t.Id descending
            select t;
            return query.ToList();
        }

        public Transaktion GetTransaktionById(long id)
        {
            Transaktion k = Context.Transaktionen.SingleOrDefault(transaktion => transaktion.Id == id);
            if (k == null)
            {
                throw new InvalidOperationException($"Transaktion with id {id} is not in database");
            }
            return k;
        }

        public Transaktion AddTransaktion(Transaktion t)
        {
            Context.Add(t);
            Maschine m = Context.Maschinen.SingleOrDefault(maschine => maschine.Id == t.MaschinenId);
            Kunde dukoStapler = Context.Kunden.SingleOrDefault(kunde => kunde.Id == 1);
            if (t.Typ == Transaktion.TransaktionsTyp.Einkauf)
            {
                m.Besitzer = dukoStapler;
            } else if (t.Typ == Transaktion.TransaktionsTyp.Verkauf)
            {
                m.Besitzer = t.Kunde;
            } else
            {
                Context.Remove(t);
            }
            Context.SaveChanges();
            return t;
        }

        public Transaktion UpdateTransaktion(Transaktion k)
        {
            var group = Context.Transaktionen.First(transaktion => transaktion.Id == k.Id);
            Context.Entry(group).CurrentValues.SetValues(k);
            Context.SaveChanges();
            return k;
        }

        public List<Transaktion> GetServiceSearchResult(ServiceSearchDto searchEntity)
        {


            var query = from t in Context.Transaktionen.Include(tra => tra.Maschine)
                where searchEntity.KundenId == null || searchEntity.KundenId == t.KundenId
                where searchEntity.MaschinenId == null || searchEntity.MaschinenId == t.MaschinenId
                where searchEntity.MaschinentypId == null || searchEntity.MaschinentypId == t.Maschine.MaschinentypId
                where searchEntity.Von == null || searchEntity.Von <= t.Datum
                where searchEntity.Bis == null || t.Datum <= searchEntity.Bis 
                orderby t.Datum descending 
                select t;

            return query.ToList();
        }
    }
}
