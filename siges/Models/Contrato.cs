using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Models {
  public class Contrato {
    public int Id { get; set; }

    [DisplayName("Contrato")]
    [StringLength(30, ErrorMessage = "Longitud máxima requerida, {1} caracteres.")]
    public string Nombre { get; set; }
    public string Tipo { get; set; }
    public bool Estatus { get; set; }

    [DisplayName("Campo Opcional 1")]
    public string Opcional1 { get; set; }

    [DisplayName("Campo Opcional 2")]
    public string Opcional2 { get; set; }

    [DisplayName("Cliente relacionado")]
    public int ClienteId { get; set; }
    public virtual Cliente Cliente { get; set; }

    [DisplayName("Tipos de servicio")]
    public int ServicioId { get; set; }
    public virtual Servicio Servicio { get; set; }

    public Contrato Copia(){
      Contrato copia = new Contrato();
      copia.Id = this.Id;
      copia.Nombre = this.Nombre;
      copia.Tipo = this.Tipo;
      copia.Estatus = this.Estatus;
      copia.Opcional1 = this.Opcional1;
      copia.Opcional2 = this.Opcional2;
      copia.ClienteId = this.ClienteId;
      copia.ServicioId = this.ServicioId;
      return copia;
    }

    public override bool Equals(Object obj){
      if(obj == null || GetType() != obj.GetType())
        return false;
      Contrato c = obj as Contrato;
      if((Object) c == null)
        return false;
      if(this.Id != c.Id)
        return false;
      if(!(String.Equals(this.Nombre, c.Nombre)))
        return false;
      if(!(String.Equals(this.Tipo, c.Tipo)))
        return false;
      if(this.Estatus != c.Estatus)
        return false;
      if(!(String.Equals(this.Opcional1, c.Opcional1)))
        return false;
      if(!(String.Equals(this.Opcional2, c.Opcional2)))
        return false;
      if(this.ClienteId != c.ClienteId)
        return false;
      if(this.ServicioId != c.ServicioId)
        return false;
      return true;
    }
  }
}
