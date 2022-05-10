using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Models
{
  public class Producto {
    public int Id { get; set; }
    [DisplayName("Producto")]
    public string Nombre { get; set; }
    public string Marca { get; set; }
    public DateTime FechaAlta {get; set;}
    public bool Estatus { get; set; }
    [DisplayName("Opcional 1")]
    public string Opcional1 { get; set; }
    [DisplayName("Opcional 2")]
    public string Opcional2 { get; set; }
  }
}
