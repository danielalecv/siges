using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.DTO
{
    public class DetalleConfiguracionServicioDTO
    {
        public int Id { get; set; }
        public int LineaNegocio { get; set; }
        public int Servicio { get; set; }
        public decimal CostoServicio { get; set; }
        public decimal PrecioServicio { get; set; }
        public int MinimoServicio { get; set; }
        public int MaximoServicio { get; set; }
        public string Opcional1 { get; set; }
        public int Opcional2 { get; set; }
    }
}
