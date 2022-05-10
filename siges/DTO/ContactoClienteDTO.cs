using System.Collections.Generic;

using siges.Models;

namespace siges.DTO{
  public class ContactoClienteDTO{
    public int clienteId {get;set;}
    public List<Persona> contactos {get;set;}
    public string opcional1 {get;set;}
    public string opcional2 {get;set;}
  }
}
