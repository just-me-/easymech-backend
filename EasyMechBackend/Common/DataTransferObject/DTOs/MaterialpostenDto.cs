﻿namespace EasyMechBackend.Common.DataTransferObject.DTOs
{
    public class MaterialpostenDto
    {
        public long Id { get; set; }
        public double Stueckpreis { get; set; }
        public int Anzahl { get; set; }
        public string Bezeichnung { get; set; }
        public long ServiceId { get; set; }
    }
}
