using System;
using siges.Models;

namespace siges.Models{
  public class Lote{
    public int Id {get; set;}
    public string Descripcion {get; set;}
    public Insumo Insumo {get; set;}
    public DateTime Caducidad {get; set;}
    public Persona Persona {get; set;}
    public bool Estatus {get; set;}
    public DateTime FechaAlta {get; set;}
    public int Cantidad {get; set;}
    public string Observaciones {get; set;}
  }
}
