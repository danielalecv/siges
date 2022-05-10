using System;

namespace siges.DTO
{
    public class IndexListOSDTO
    {
        public int Id { get; set; }
        public string Folio { get; set; }
        public DateTime? FechaInicio { get; set; }
        public string Cliente { get; set; }
        public string Contrato { get; set; }
        public string ContratoTipo { get; set; }
        public string Ubicacion { get; set; }
        public string Servicio { get; set; }
        public string EstatusServicio { get; set; }
    }
}
