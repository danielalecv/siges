using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Models
{
    public class EntradaActivoFijo
    {
        public int Id { get; set; }
        public string Remision { get; set; }
        public string Tipo { get; set; }
        public Ubicacion Ubicacion { get; set; }
        public DateTime FechaRemision { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public bool Incidencia { get; set; }
        public string Observaciones { get; set; }
        public List<DetalleActivoFijo> Desglose { get; set; }
        public bool Estatus { get; set; }

        public EntradaActivoFijo()
        {
            this.Ubicacion = new Ubicacion();
            this.Desglose = new List<DetalleActivoFijo>();
        }
    }
}
