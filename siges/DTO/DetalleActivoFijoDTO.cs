using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.DTO
{
    public class DetalleActivoFijoDTO
    {
        public int Id { get; set; }
        public int Referencia { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public string NumeroSerie { get; set; }
        public string Unidad { get; set; }
        public bool Arrendamiento { get; set; }
        public string Observaciones { get; set; }
    }
}
