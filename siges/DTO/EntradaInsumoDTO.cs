using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.DTO
{
    public class EntradaInsumoDTO
    {
        public int Id { get; set; }
        public string Pedido { get; set; }
        public string Tipo { get; set; }
        public int Ubicacion { get; set; }
        public string FechaPedido { get; set; }
        public string FechaRecepcion { get; set; }
        public bool Incidencia { get; set; }
        public string Observaciones { get; set; }
        public List<DetalleInsumoDTO> Desglose { get; set; }
        public bool Estatus { get; set; }
    }
}
