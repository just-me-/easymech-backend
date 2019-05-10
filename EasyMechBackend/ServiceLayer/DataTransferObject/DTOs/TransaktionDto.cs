﻿using System;
using EasyMechBackend.DataAccessLayer.Entities;

namespace EasyMechBackend.ServiceLayer.DataTransferObject.DTOs
{
    public class TransaktionDto : DtoBase
    {
        public long Id { get; set; }
        public double Preis { get; set; }
        public Transaktion.TransaktionsTyp Typ { get; set; }
        public DateTime? Datum { get; set; }
        public long MaschinenId { get; set; }
        public long? KundenId { get; set; }
    }
}