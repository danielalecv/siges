using System;
using siges.Models;

namespace siges.Models{
  public class InventarioI{
    public int Id {get; set;}
    public Insumo Insumo {get;set;}
    public Lote Lote {get; set;}
    public Persona Persona {get; set;}
    public bool Estatus {get; set;}
    public DateTime FechaAlta {get; set;}
    public int Cantidad {get; set;}
    public string Observaciones {get; set;}
  }
}
