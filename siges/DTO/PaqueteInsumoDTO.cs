using System;
using System.Collections.Generic;
namespace siges.DTO {
  public class PaqueteInsumoDTO {
    public string ClasificacionPaquete {get; set;}
    public string DescripcionPaquete {get; set;}
    public string JsonInsumos {get; set;}
    public int InsumoId {get; set;}
    public int PaqueteId {get; set;}
    public int Cantidad {get; set;}
  }
}
