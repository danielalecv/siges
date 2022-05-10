using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace siges.DTO {
  public class CapturaDatosFacturaDTO {
    public int OrdenServicioId {get; set;}
    public string FacturaFolio {get; set;}
    public DateTime FacturaFecha {get; set;}
  }
}
