using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Models {
  public class Ubicacion : IComparable<Ubicacion>{
    public int Id { get; set; }
    [DisplayName("Ubicación")]
    public string Nombre { get; set; }
    [DisplayName("Dirección")]
    public string Direccion { get; set; }
    [DisplayName("Nombre del contacto")]
    public string Contacto { get; set; }
    [DisplayName("Teléfono de contacto")]
    public string ContactoTelefono { get; set; }
    [DisplayName("Email de contacto")]
    public string ContactoEmail { get; set; }
    public string Latitude {get; set;}
    public string Longitude {get; set;}
    [DisplayName("Contacto Opcional")]
    public string ContactoOpcional { get; set; }
    [DisplayName("Contacto Teléfono Opcional")]
    public string ContactoOpcionalTelefono { get; set; }
    [DisplayName("Contacto Email Opcional")]
    public string ContactoOpcionalEmail { get; set; }
    public bool Estatus { get; set; }
    public float Distancia { get;set; }

    [DisplayName("Cliente relacionado")]
    public int ClienteId { get; set; }
    public virtual Cliente Cliente { get; set; }

    public Ubicacion Copia(){
      Ubicacion copia = new Ubicacion();
      copia.Id = this.Id;
      copia.Nombre = this.Nombre;
      copia.Direccion = this.Direccion;
      copia.Contacto = this.Contacto;
      copia.ContactoTelefono = this.ContactoTelefono;
      copia.ContactoEmail = this.ContactoEmail;
      copia.ContactoOpcional = this.ContactoOpcional;
      copia.ContactoOpcionalTelefono = this.ContactoOpcionalTelefono;
      copia.ContactoOpcionalEmail = this.ContactoOpcionalEmail;
      copia.Estatus = this.Estatus;
      copia.ClienteId = this.ClienteId;
      return copia;
    }

    public override bool Equals(Object obj){
      if(obj == null || GetType() != obj.GetType())
        return false;
      Ubicacion u = obj as Ubicacion;
      if((Object)u == null)
        return false;
      if( this.Id != u.Id)
        return false;
      if(!(String.Equals(this.Nombre, u.Nombre)))
        return false;
      if(!(String.Equals(this.Direccion, u.Direccion)))
        return false;
      if(!(String.Equals(this.Contacto, u.Contacto)))
        return false;
      if(!(String.Equals(this.ContactoTelefono, u.ContactoTelefono)))
        return false;
      if(!(String.Equals(this.ContactoEmail, u.ContactoEmail)))
        return false;
      if(!(String.Equals(this.ContactoOpcional, u.ContactoOpcional)))
        return false;
      if(!(String.Equals(this.ContactoOpcionalTelefono, u.ContactoOpcionalTelefono)))
        return false;
      if(!(String.Equals(this.ContactoOpcionalEmail, u.ContactoOpcionalEmail)))
        return false;
      if(this.Estatus != u.Estatus)
        return false;
      if(this.ClienteId != u.ClienteId)
        return false;
      return true;
    }

    public int CompareTo(Ubicacion u){
      if(u == null)
        return 1;
      else
        return u.Nombre.CompareTo(this.Nombre);
    }
  }
}
