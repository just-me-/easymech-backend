using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using EasyMechBackend.DataAccessLayer.Entities;
using EasyMechBackend.ServiceLayer.DataTransferObject.DTOs;

namespace EasyMechBackend.ServiceLayer.DataTransferObject
{
    public static class DtoConverter
    {

        #region Kunde

        public static Kunde ConvertToEntity(this KundeDto dto)
        {
            if (dto == null) { return null; }

            Kunde k = new Kunde
            {
                Id = dto.Id,
                Firma = dto.Firma,
                Vorname = dto.Vorname,
                Nachname = dto.Nachname,
                Adresse = dto.Adresse,
                PLZ = dto.PLZ,
                Ort = dto.Ort,
                Email = dto.Email,
                Telefon = dto.Telefon,
                Notiz = dto.Notiz,
                IstAktiv = dto.IstAktiv
            };
            return k;
        }

        public static KundeDto ConvertToDto(this Kunde entity)
        {
            if (entity == null) { return null; }

            KundeDto dto = new KundeDto
            {
                Id = entity.Id,
                Firma = entity.Firma,
                Vorname = entity.Vorname,
                Nachname = entity.Nachname,
                Adresse = entity.Adresse,
                PLZ = entity.PLZ,
                Ort = entity.Ort,
                Email = entity.Email,
                Telefon = entity.Telefon,
                Notiz = entity.Notiz,
                IstAktiv = entity.IstAktiv
            };

            return dto;
        }



        public static List<Kunde> ConvertToEntities(this IEnumerable<KundeDto> dtos)
        {
            return ConvertGenericList(dtos, ConvertToEntity);
        }
        public static List<KundeDto> ConvertToDtos(this IEnumerable<Kunde> entities)
        {
            return ConvertGenericList(entities, ConvertToDto);
        }

        #endregion
        #region Maschine
        public static Maschine ConvertToEntity(this MaschineDto dto)
        {
            if (dto == null) { return null; }

            Maschine m = new Maschine
            {
                Id = dto.Id,
                Seriennummer = dto.Seriennummer,
                Mastnummer = dto.Mastnummer,
                Motorennummer = dto.Motorennummer,
                Betriebsdauer = dto.Betriebsdauer,
                Jahrgang = dto.Jahrgang,
                Notiz = dto.Notiz,
                IstAktiv = dto.IstAktiv,
                BesitzerId = dto.BesitzerId,
                MaschinentypId = dto.MaschinentypId
            };
            return m;
        }

        public static MaschineDto ConvertToDto(this Maschine entity)
        {
            if (entity == null) { return null; }

            MaschineDto dto = new MaschineDto
            {
                Id = entity.Id,
                Seriennummer = entity.Seriennummer,
                Mastnummer = entity.Mastnummer,
                Motorennummer = entity.Motorennummer,
                Betriebsdauer = entity.Betriebsdauer,
                Jahrgang = entity.Jahrgang,
                Notiz = entity.Notiz,
                IstAktiv = entity.IstAktiv,
                BesitzerId = entity.BesitzerId,
                MaschinentypId = entity.MaschinentypId
            };
            return dto;
        }

        public static List<Maschine> ConvertToEntities(this IEnumerable<MaschineDto> dtos)
        {
            return ConvertGenericList(dtos, ConvertToEntity);
        }
        public static List<MaschineDto> ConvertToDtos(this IEnumerable<Maschine> entities)
        {
            return ConvertGenericList(entities, ConvertToDto);
        }

        #endregion
        #region Maschinentyp
        public static Maschinentyp ConvertToEntity(this MaschinentypDto dto)
        {
            if (dto == null) { return null; }

            Maschinentyp f = new Maschinentyp
            {
                Id = dto.Id,
                Fabrikat = dto.Fabrikat,
                Motortyp = dto.Motortyp,
                Nutzlast = dto.Nutzlast,
                Hubkraft = dto.Hubkraft,
                Hubhoehe = dto.Hubhoehe,
                Eigengewicht = dto.Eigengewicht,
                Maschinenhoehe = dto.Maschinenhoehe,
                Maschinenlaenge = dto.Maschinenlaenge,
                Maschinenbreite = dto.Maschinenbreite,
                Pneugroesse = dto.Pneugroesse
            };
            return f;
        }

        public static MaschinentypDto ConvertToDto(this Maschinentyp entity)
        {
            if (entity == null) { return null; }

            MaschinentypDto dto = new MaschinentypDto
            {
                Id = entity.Id,
                Fabrikat = entity.Fabrikat,
                Motortyp = entity.Motortyp,
                Nutzlast = entity.Nutzlast,
                Hubkraft = entity.Hubkraft,
                Hubhoehe = entity.Hubhoehe,
                Eigengewicht = entity.Eigengewicht,
                Maschinenhoehe = entity.Maschinenhoehe,
                Maschinenlaenge = entity.Maschinenlaenge,
                Maschinenbreite = entity.Maschinenbreite,
                Pneugroesse = entity.Pneugroesse
            };
            return dto;
        }

        public static List<Maschinentyp> ConvertToEntities(this IEnumerable<MaschinentypDto> dtos)
        {
            return ConvertGenericList(dtos, ConvertToEntity);
        }
        public static List<MaschinentypDto> ConvertToDtos(this IEnumerable<Maschinentyp> entities)
        {
            return ConvertGenericList(entities, ConvertToDto);
        }

        #endregion
        #region Transaktion

        public static Transaktion ConvertToEntity(this TransaktionDto dto)
        {
            if (dto == null) { return null; }

            Transaktion t = new Transaktion
            {
                Id = dto.Id,
                Preis = dto.Preis,
                Typ = dto.Typ,
                Datum = dto.Datum,
                MaschinenId = dto.MaschinenId,
                KundenId = dto.KundenId
            };
            return t;
        }

        public static TransaktionDto ConvertToDto(this Transaktion entity)
        {
            if (entity == null) { return null; }

            TransaktionDto dto = new TransaktionDto
            {
                Id = entity.Id,
                Preis = entity.Preis,
                Typ = entity.Typ,
                Datum = entity.Datum,
                MaschinenId = entity.MaschinenId,
                KundenId = entity.KundenId
            };

            return dto;
        }



        public static List<Transaktion> ConvertToEntities(this IEnumerable<TransaktionDto> dtos)
        {
            return ConvertGenericList(dtos, ConvertToEntity);
        }
        public static List<TransaktionDto> ConvertToDtos(this IEnumerable<Transaktion> entities)
        {
            return ConvertGenericList(entities, ConvertToDto);
        }

        #endregion
        /*
        #region Reservation

        public static Reservation ConvertToEntity(this ReservationDto dto)
        {
            if (dto == null) { return null; }

            Reservation t = new Reservation
            {
                Id = dto.Id,
                MaschinenId = dto.MaschinenId,
                KundenId = dto.KundenId,
                Startdatum = dto.Startdatum,
                Enddatum = dto.Enddatum,
                Ruecknahme = dto.Ruecknahme.ConvertToEntity(),
                Uebergabe = dto.Uebergabe.ConvertToEntity(),
                Standort = dto.Standort
            };
            return t;
        }

        public static ReservationDto ConvertToDto(this Reservation entity)
        {
            if (entity == null) { return null; }

            ReservationDto dto = new ReservationDto
            {
                Id = entity.Id,
                MaschinenId = entity.MaschinenId,
                KundenId = entity.KundenId,
                Startdatum = entity.Startdatum,
                Enddatum = entity.Enddatum,
                Ruecknahme = entity.Ruecknahme.ConvertToDto(),
                Uebergabe = entity.Uebergabe.ConvertToDto(),
                Standort = entity.Standort
            };

            return dto;
        }

        public static List<Reservation> ConvertToEntities(this IEnumerable<ReservationDto> dtos)
        {
            return ConvertGenericList(dtos, ConvertToEntity);
        }
        public static List<ReservationDto> ConvertToDtos(this IEnumerable<Reservation> entities)
        {
            return ConvertGenericList(entities, ConvertToDto);
        }

        #endregion
        #region ResrevatrionsUebergabe

        public static MaschinenUebergabe ConvertToEntity(this MaschinenUebergabeDto dto)
        {
            if (dto == null) { return null; }

            MaschinenUebergabe t = new MaschinenUebergabe
            {
                Id = dto.Id,
                Datum = dto.Datum,
                ReservationsId = dto.ReservationsId
            };
            return t;
        }

        public static MaschinenUebergabeDto ConvertToDto(this MaschinenUebergabe entity)
        {
            if (entity == null) { return null; }


            MaschinenUebergabeDto dto = new MaschinenUebergabeDto
            {
                Id = entity.Id,
                Datum = entity.Datum,
                ReservationsId = entity.ReservationsId
            };
            return dto;
        }

        #endregion
        #region ResrevatrionsRuecknahme


        public static MaschinenRuecknahme ConvertToEntity(this MaschinenRuecknahmeDto dto)
        {
            if (dto == null) { return null; }

            MaschinenRuecknahme t = new MaschinenRuecknahme
            {
                Id = dto.Id,
                Datum = dto.Datum,
                ReservationsId = dto.ReservationsId
            };
            return t;
        }

        public static MaschinenRuecknahmeDto ConvertToDto(this MaschinenRuecknahme entity)
        {
            if (entity == null) { return null; }


            MaschinenRuecknahmeDto dto = new MaschinenRuecknahmeDto
            {
                Id = entity.Id,
                Datum = entity.Datum,
                ReservationsId = entity.ReservationsId
            };
            return dto;
        }

        #endregion
        */

        private static List<TTarget> ConvertGenericList<TSource, TTarget>(this IEnumerable<TSource> source, Func<TSource, TTarget> converter)
        {
            if (source == null) { return null; }
            if (converter == null) { return null; }

            return source.Select(converter).ToList();
        }
    }



}
