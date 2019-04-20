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

            Kunde k = new Kunde();
            k.Id = dto.Id;
            k.Firma = dto.Firma;
            k.Vorname = dto.Vorname;
            k.Nachname = dto.Nachname;
            k.Adresse = dto.Adresse;
            k.PLZ = dto.PLZ;
            k.Ort = dto.Ort;
            k.Email = dto.Email;
            k.Telefon = dto.Telefon;
            k.Notiz = dto.Notiz;
            k.IsActive = dto.IsActive;
            return k;
        }

        public static KundeDto ConvertToDto(this Kunde entity)
        {
            if (entity == null) { return null; }

            KundeDto dto = new KundeDto();
            dto.Id = entity.Id;
            dto.Firma = entity.Firma;
            dto.Vorname = entity.Vorname;
            dto.Nachname = entity.Nachname;
            dto.Adresse = entity.Adresse;
            dto.PLZ = entity.PLZ;
            dto.Ort = entity.Ort;
            dto.Email = entity.Email;
            dto.Telefon = entity.Telefon;
            dto.Notiz = entity.Notiz;
            dto.IsActive = entity.IsActive;

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

            Maschine m = new Maschine();
            m.Id = dto.Id;
            m.Seriennummer = dto.Seriennummer;
            m.Mastnummer = dto.Mastnummer;
            m.Motorennummer = dto.Motorennummer;
            m.Betriebsdauer = dto.Betriebsdauer;
            m.Jahrgang = dto.Jahrgang;
            m.Notiz = dto.Notiz;
            m.IsActive = dto.IsActive;
            return m;
        }

        public static MaschineDto ConvertToDto(this Maschine entity)
        {
            if (entity == null) { return null; }

            MaschineDto dto = new MaschineDto();
            dto.Id = entity.Id;
            dto.Seriennummer = entity.Seriennummer;
            dto.Mastnummer = entity.Mastnummer;
            dto.Motorennummer = entity.Motorennummer;
            dto.Betriebsdauer = entity.Betriebsdauer;
            dto.Jahrgang = entity.Jahrgang;
            dto.Notiz = entity.Notiz;
            dto.IsActive = entity.IsActive;
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

            Fahrzeugtyp f = new Fahrzeugtyp();
            f.Id = dto.Id;
            f.Fabrikat = dto.Fabrikat;
            f.Motortyp = dto.Fabrikat;
            f.Nutzlast = dto.Nutzlast;
            f.Hubkraft = dto.Hubkraft;
            f.Hubhöhe = dto.Hubhöhe;
            f.Eigengewicht = dto.Eigengewicht;
            f.Fahrzeughöhe = dto.Fahrzeughöhe;
            f.Fahrzeuglänge = dto.Fahrzeuglänge;
            f.Fahrzeugbreite = dto.Fahrzeugbreite;
            f.Pneugrösse = dto.Pneugrösse;
            return f;
        }

        public static FahrzeugtypDto ConvertToDto(this Fahrzeugtyp entity)
        {
            if (entity == null) { return null; }

            FahrzeugtypDto dto = new FahrzeugtypDto();
            dto.Id = entity.Id;
            dto.Fabrikat = entity.Fabrikat;
            dto.Motortyp = entity.Fabrikat;
            dto.Nutzlast = entity.Nutzlast;
            dto.Hubkraft = entity.Hubkraft;
            dto.Hubhöhe = entity.Hubhöhe;
            dto.Eigengewicht = entity.Eigengewicht;
            dto.Fahrzeughöhe = entity.Fahrzeughöhe;
            dto.Fahrzeuglänge = entity.Fahrzeuglänge;
            dto.Fahrzeugbreite = entity.Fahrzeugbreite;
            dto.Pneugrösse = entity.Pneugrösse;
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
