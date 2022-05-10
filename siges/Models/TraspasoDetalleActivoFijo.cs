using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Models
{
    public class TraspasoDetalleActivoFijo
    {
        public int Id { get; set; }
        public ActivoFijo ActivoFijo { get; set; }
        public bool Estatus { get; set; }

        public TraspasoDetalleActivoFijo()
        {
            this.ActivoFijo = new ActivoFijo();
        }
    }
}
