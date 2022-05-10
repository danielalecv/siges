using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Models
{
    public class DetalleConfiguracionServicio
    {
        public int Id { get; set; }
        public LineaNegocio LineaNegocio { get; set; }
        public Servicio Servicio { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        [DataType(DataType.Currency)]
        public decimal CostoServicio { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        [DataType(DataType.Currency)]
        public decimal PrecioServicio { get; set; }
        public int MinimoServicio { get; set; }
        public int MaximoServicio { get; set; }
        public string Opcional1 { get; set; }
        public int Opcional2 { get; set; }
        public bool Estatus { get; set; }

        public DetalleConfiguracionServicio()
        {
            LineaNegocio = new LineaNegocio();
            Servicio = new Servicio();
        }
    }
}
