using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Models {
  public class ConfiguracionContrato {
    public int Id { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    [DataType(DataType.Currency)]
    public decimal CostoServicio { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    [DataType(DataType.Currency)]
    public decimal PrecioServicio { get; set; }
    public int MinimoServicio { get; set; }
    public int MaximoServicio { get; set; }
  }
}
