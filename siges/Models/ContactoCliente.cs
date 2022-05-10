using System;
using System.Collections.Generic;

using siges.Areas.Identity.Data;

namespace siges.Models{
  public class ContactoCliente{
    public int Id {get;set;}
    public bool Estatus {get;set;}
    public Cliente Cliente {get;set;}
    public List<Persona> Contactos {get;set;}
    public RoatechIdentityUser CreadorPor {get;set;}
    public RoatechIdentityUser ModificadoPor {get;set;}
    public DateTime FechaCreacion {get;set;}
    public DateTime FechaModificacion {get;set;}
    public string Opcional1 {get;set;}
    public string Opcional2 {get;set;}
    public ContactoCliente() {
      this.Contactos = new List<Persona>();
    }
  }
}
