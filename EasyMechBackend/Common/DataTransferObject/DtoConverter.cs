using System;
using System.Collections.Generic;
using System.Linq;
using EasyMechBackend.Common.DataTransferObject.DTOs;
using EasyMechBackend.DataAccessLayer.Entities;

namespace EasyMechBackend.Common.DataTransferObject
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
                Datum = dto.Datum == DateTime.MinValue ? new DateTime(1900,1,1) : dto.Datum,
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
        #region ReservationsÜbergabe

        public static MaschinenUebergabe ConvertToEntity(this MaschinenUebergabeDto dto)
        {
            if (dto == null) { return null; }
            if (dto.Datum == DateTime.MinValue) { return null; }

            MaschinenUebergabe t = new MaschinenUebergabe
            {
                Datum = dto.Datum,
                Notiz = dto.Notiz
            };
            return t;
        }

        public static MaschinenUebergabeDto ConvertToDto(this MaschinenUebergabe entity)
        {
            if (entity == null) { return null; }


            MaschinenUebergabeDto dto = new MaschinenUebergabeDto
            {
                Datum = entity.Datum,
                Notiz = entity.Notiz
            };
            return dto;
        }

        #endregion
        #region ReservationsRücknahme


        public static MaschinenRuecknahme ConvertToEntity(this MaschinenRuecknahmeDto dto)
        {
            if (dto == null) { return null; }

            if (dto.Datum == DateTime.MinValue) { return null; }

            MaschinenRuecknahme t = new MaschinenRuecknahme
            {
                Datum = dto.Datum,
                Notiz = dto.Notiz
            };
            return t;
        }

        public static MaschinenRuecknahmeDto ConvertToDto(this MaschinenRuecknahme entity)
        {
            if (entity == null) { return null; }


            MaschinenRuecknahmeDto dto = new MaschinenRuecknahmeDto
            {
                Datum = entity.Datum,
                Notiz = entity.Notiz
            };
            return dto;
        }

        #endregion
        #region Service
        public static Service ConvertToEntity(this ServiceDto dto)
        {
            if (dto == null) { return null; }
            if (dto.Beginn == DateTime.MinValue) dto.Beginn = new DateTime(1900, 1, 1);
            if (dto.Ende == DateTime.MinValue) dto.Ende = new DateTime(2999, 1, 1);
            List<Arbeitsschritt> arbeitsschritte = new List<Arbeitsschritt>();
            List<Materialposten> materialposten = new List<Materialposten>();

            if (dto.Arbeitsschritte != null)
            {
                foreach (ArbeitsschrittDto aDto in dto.Arbeitsschritte)
                {
                    Arbeitsschritt a = new Arbeitsschritt
                    {
                        Id = aDto.Id,
                        Bezeichnung = aDto.Bezeichnung,
                        Stundenansatz = aDto.Stundenansatz,
                        Arbeitsstunden = aDto.Arbeitsstunden,
                        ServiceId = aDto.ServiceId
                    };
                    arbeitsschritte.Add(a);
                }
            }

            if (dto.Materialposten != null)
            {
                foreach (MaterialpostenDto mDto in dto.Materialposten)
                {
                    Materialposten m = new Materialposten
                    {
                        Id = mDto.Id,
                        Bezeichnung = mDto.Bezeichnung,
                        Stueckpreis = mDto.Stueckpreis,
                        Anzahl = mDto.Anzahl,
                        ServiceId = mDto.ServiceId
                    };
                    materialposten.Add(m);
                }
            }

            Service t = new Service
            {
                Id = dto.Id,
                Bezeichnung = dto.Bezeichnung,
                Beginn = dto.Beginn,
                Ende = dto.Ende,
                Status = dto.Status,
                MaschinenId = dto.MaschinenId,
                KundenId = dto.KundenId,
                Materialposten = materialposten,
                Arbeitsschritte = arbeitsschritte
            };
            return t;
        }

        public static ServiceDto ConvertToDto(this Service entity)
        {
            if (entity == null) { return null; }
            List<ArbeitsschrittDto> arbeitsschritte = new List<ArbeitsschrittDto>();
            List<MaterialpostenDto> materialposten = new List<MaterialpostenDto>();
            foreach (Arbeitsschritt a in entity.Arbeitsschritte)
            {
                ArbeitsschrittDto aDto = new ArbeitsschrittDto
                {
                    Id = a.Id,
                    Bezeichnung = a.Bezeichnung,
                    Stundenansatz = a.Stundenansatz,
                    Arbeitsstunden = a.Arbeitsstunden,
                    ServiceId = a.ServiceId
                };
                arbeitsschritte.Add(aDto);
            }
            foreach (Materialposten m in entity.Materialposten)
            {
                MaterialpostenDto mDto = new MaterialpostenDto
                {
                    Id = m.Id,
                    Bezeichnung = m.Bezeichnung,
                    Stueckpreis = m.Stueckpreis,
                    Anzahl = m.Anzahl,
                    ServiceId = m.ServiceId
                };
                materialposten.Add(mDto);
            }

            ServiceDto s = new ServiceDto
            {
                Id = entity.Id,
                Bezeichnung = entity.Bezeichnung,
                Beginn = entity.Beginn,
                Ende = entity.Ende,
                Status = entity.Status,
                MaschinenId = entity.MaschinenId,
                KundenId = entity.KundenId,
                Materialposten = materialposten,
                Arbeitsschritte = arbeitsschritte
            };
            return s;
        }

        public static List<Service> ConvertToEntities(this IEnumerable<ServiceDto> dtos)
        {
            return ConvertGenericList(dtos, ConvertToEntity);
        }
        public static List<ServiceDto> ConvertToDtos(this IEnumerable<Service> entities)
        {
            return ConvertGenericList(entities, ConvertToDto);
        }
        #endregion

        private static List<TTarget> ConvertGenericList<TSource, TTarget>(this IEnumerable<TSource> source, Func<TSource, TTarget> converter)
        {
            if (source == null) { return null; }
            if (converter == null) { return null; }

            return source.Select(converter).ToList();
        }
    }

}
