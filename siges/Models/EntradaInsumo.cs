using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Models
{
    public class EntradaInsumo
    {
        public int Id { get; set; }
        public string Pedido { get; set; }
        public string Tipo { get; set; }
        public Ubicacion Ubicacion { get; set; }
        public DateTime FechaPedido { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public bool Incidencia { get; set; }
        public string Observaciones { get; set; }
        public List<DetalleInsumo> Desglose { get; set; }
        public bool Estatus { get; set; }
    }
}
