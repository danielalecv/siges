using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Models
{
    public class DetalleInsumo
    {
        public int Id { get; set; }
        public Insumo Referencia { get; set; }
        public string ClaveInsumo { get; set; }
        public int Cantidad { get; set; }
        public string Unidad { get; set; }
        public string Observaciones { get; set; }
        public bool Estatus { get; set; }
    }
}
