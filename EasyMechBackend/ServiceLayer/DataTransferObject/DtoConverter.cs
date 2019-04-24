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
                IstAktiv = dto.IstAktiv
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
                IstAktiv = entity.IstAktiv
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
        #region Fahrzeugtyp
        public static Fahrzeugtyp ConvertToEntity(this FahrzeugtypDto dto)
        {
            if (dto == null) { return null; }

            Fahrzeugtyp f = new Fahrzeugtyp
            {
                Id = dto.Id,
                Fabrikat = dto.Fabrikat,
                Motortyp = dto.Motortyp,
                Nutzlast = dto.Nutzlast,
                Hubkraft = dto.Hubkraft,
                Hubhoehe = dto.Hubhoehe,
                Eigengewicht = dto.Eigengewicht,
                Fahrzeughoehe = dto.Fahrzeughoehe,
                Fahrzeuglaenge = dto.Fahrzeuglaenge,
                Fahrzeugbreite = dto.Fahrzeugbreite,
                Pneugroesse = dto.Pneugroesse
            };
            return f;
        }

        public static FahrzeugtypDto ConvertToDto(this Fahrzeugtyp entity)
        {
            if (entity == null) { return null; }

            FahrzeugtypDto dto = new FahrzeugtypDto
            {
                Id = entity.Id,
                Fabrikat = entity.Fabrikat,
                Motortyp = entity.Motortyp,
                Nutzlast = entity.Nutzlast,
                Hubkraft = entity.Hubkraft,
                Hubhoehe = entity.Hubhoehe,
                Eigengewicht = entity.Eigengewicht,
                Fahrzeughoehe = entity.Fahrzeughoehe,
                Fahrzeuglaenge = entity.Fahrzeuglaenge,
                Fahrzeugbreite = entity.Fahrzeugbreite,
                Pneugroesse = entity.Pneugroesse
            };
            return dto;
        }

        public static List<Fahrzeugtyp> ConvertToEntities(this IEnumerable<FahrzeugtypDto> dtos)
        {
            return ConvertGenericList(dtos, ConvertToEntity);
        }
        public static List<FahrzeugtypDto> ConvertToDtos(this IEnumerable<Fahrzeugtyp> entities)
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
