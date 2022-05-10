using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.DTO
{
    public class DetalleInsumoDTO
    {
        public int Id { get; set; }
        public int Referencia { get; set; }
        public string ClaveInsumo { get; set; }
        public int Cantidad { get; set; }
        public string Unidad { get; set; }
        public string Observaciones { get; set; }
    }
}
