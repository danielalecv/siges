using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Models
{
    public class InventarioActivoFijo
    {
        public int Id { get; set; }
        public ActivoFijo ActivoFijo { get; set; }
        public int Teorico { get; set; }
        public int Fisico { get; set; }
        public int Ajuste { get; set; }
        public bool Estatus { get; set; }
        public InventarioActivoFijo(){
            this.ActivoFijo = new ActivoFijo();
        }
    }
}
