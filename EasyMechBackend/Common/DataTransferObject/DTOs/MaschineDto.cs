using EasyMechBackend.ServiceLayer.DataTransferObject;

namespace EasyMechBackend.Common.DataTransferObject.DTOs
{
    public class MaschineDto : DtoBase
    {
        public long Id { get; set; }
        public string Seriennummer { get; set; }
        public string Mastnummer { get; set; }
        public string Motorennummer { get; set; }
        public int? Betriebsdauer { get; set; }
        public int? Jahrgang { get; set; }
        public string Notiz { get; set; }
        public bool? IstAktiv { get; set; }
        public long BesitzerId { get; set; }
        public long MaschinentypId { get; set; }
    }
}
