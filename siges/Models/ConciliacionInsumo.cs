using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Models
{
    public class ConciliacionInsumo
    {
        public int Id { get; set; }
        public string Folio { get; set; }
        public DateTime Fecha { get; set; }
        public Ubicacion Ubicacion { get; set; }
        public List<InventarioInsumo> Detalle { get; set; }
        public bool Estatus { get; set; }
    }
}
