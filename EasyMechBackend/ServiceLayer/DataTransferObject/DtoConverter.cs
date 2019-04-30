using EasyMechBackend.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

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

        private static List<TTarget> ConvertGenericList<TSource, TTarget>(this IEnumerable<TSource> source, Func<TSource, TTarget> converter)
        {
            if (source == null) { return null; }
            if (converter == null) { return null; }

            return source.Select(converter).ToList();
        }
    }



}
