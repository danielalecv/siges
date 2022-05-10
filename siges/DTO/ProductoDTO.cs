using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.DTO
{
  public class ProductoDTO
  {
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Marca { get; set; }
    public bool Estatus { get; set; }
    public string Opcional1 {get; set;}
    public string Opcional2 {get; set;}
  }
}
