using EasyMechBackend.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            k.PLZ = dto.PLZ;
            k.Ort = dto.Ort;
            k.Email = dto.Email;
            k.Telefon = dto.Telefon;
            k.Notiz = dto.Notiz;
            k.IsActive = dto.IsActive;
            k.Timestamp = dto.Timestamp;

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
            dto.PLZ = entity.PLZ;
            dto.Ort = entity.Ort;
            dto.Email = entity.Email;
            dto.Telefon = entity.Telefon;
            dto.Notiz = entity.Notiz;
            dto.IsActive = entity.IsActive;
            dto.Timestamp = entity.Timestamp;

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

        private static List<TTarget> ConvertGenericList<TSource, TTarget>(this IEnumerable<TSource> source, Func<TSource, TTarget> converter)
        {
            if (source == null) { return null; }
            if (converter == null) { return null; }

            return source.Select(converter).ToList();
        }
    }
    


}

