using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Models
{
    public class TraspasoDetalleInsumo
    {
        public int Id { get; set; }
        public Insumo Insumo { get; set; }
        public bool Estatus { get; set; }

        public TraspasoDetalleInsumo()
        {
            this.Insumo = new Insumo();
        }
    }
}
