
namespace EasyMechBackend.ServiceLayer.DataTransferObject.DTOs
{
    public class MaschinentypDto : DtoBase
    {
        public long Id { get; set; }
        public string Fabrikat { get; set; }
        public string Motortyp { get; set; }
        public int? Nutzlast { get; set; }
        public int? Hubkraft { get; set; }
        public int? Hubhoehe { get; set; }
        public int? Eigengewicht { get; set; }
        public int? Maschinenhoehe { get; set; }
        public int? Maschinenlaenge { get; set; }
        public int? Maschinenbreite { get; set; }
        public int? Pneugroesse { get; set; }
    }
}
