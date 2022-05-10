using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace siges.Models {
  public class Administracion {
    public int Id {get; set;}
    public bool Estatus {get; set;}
    public int OrdenServicioId {get; set;}
    public int PersonaId {get; set;}
    public DateTime FechaAdministrativa {get; set;}
    public string FacturaFolio {get; set;}
    public DateTime FacturaFecha {get; set;}
  }
}
