using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EasyMechBackend.DataAccessLayer;
using EasyMechBackend.DataAccessLayer.Entities;
using EasyMechBackend.Util;

namespace EasyMechBackend.ServiceLayer.DataTransferObject.DTOs
{
    public class ReservationDto
    {
        public long Id { get; set; }
        public string Standort { get; set; }

        public DateTime? Startdatum { get; set; }
        public DateTime? Enddatum { get; set; }

        public long MaschinenId { get; set; }
        public long? KundenId { get; set; }

        public MaschinenUebergabeDto Uebergabe { get; set; }
        public MaschinenRuecknahmeDto Ruecknahme { get; set; }
    }
}
