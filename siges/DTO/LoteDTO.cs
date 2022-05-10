using System;

namespace siges.DTO {
  public class LoteDTO {
    public int LoteId {get; set;}
    public int InsumoId {get; set;}
    public string Descripcion {get; set;}
    public DateTime Caducidad {get; set;}
    public int Cantidad {get; set;}
    public string Observaciones {get; set;}
  }
}
