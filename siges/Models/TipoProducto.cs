using System;

using siges.Areas.Identity.Data;

namespace siges.Models{
  public class TipoProducto{
    public int Id {get;set;}
    public string Descripcion {get;set;}
    public bool Estatus {get;set;}
    public RoatechIdentityUser CreadorPor {get;set;}
    public RoatechIdentityUser ModificadoPor {get;set;}
    public DateTime FechaCreacion {get;set;}
    public DateTime FechaModificacion {get;set;}
    public string Opcional1 {get;set;}
    public string Opcional2 {get;set;}
  }
}
