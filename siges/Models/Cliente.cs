using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace siges.Models {
  public class Cliente : IComparable<Cliente>{
    public int Id { get; set; }
    [DisplayName("Nombre o Razón Social")]
    public String RazonSocial { get; set; }
    public String RFC { get; set; }
    [DisplayName("Teléfono")]
    public String Telefono { get; set; }
    [DisplayName("Campo Opcional 1")]
    public String Opcional1 { get; set; }
    [DisplayName("Campo Opcional 2")]
    public String Opcional2 { get; set; }
    public bool Estatus { get; set; }
    [JsonIgnore]
    public ICollection<Contrato> Contratos { get; set; }
    [JsonIgnore]
    public ICollection<Ubicacion> Ubicaciones { get; set; }

    public int CompareTo(Cliente cl){
      if(cl == null)
        return 1;
      else
        return this.RazonSocial.CompareTo(cl.RazonSocial);
    }

    public Cliente Copia(){
      Cliente copia = new Cliente();
      copia.Id = this.Id;
      copia.RazonSocial = this.RazonSocial;
      copia.RFC = this.RFC;
      copia.Telefono = this.Telefono;
      copia.Opcional1 = this.Opcional1;
      copia.Opcional2 = this.Opcional2;
      copia.Estatus = this.Estatus;
      copia.Contratos = this.Contratos;
      copia.Ubicaciones = this.Ubicaciones;
      return copia;
    }

    public override bool Equals(Object obj){
      if(obj == null || GetType() != obj.GetType())
        return false;
      Cliente c = obj as Cliente;
      if((Object) c == null)
        return false;
      if(this.Id != c.Id)
        return false;
      if(!String.Equals(this.RazonSocial, c.RazonSocial))
        return false;
      if(!String.Equals(this.RFC, c.RFC))
        return false;
      if(!String.Equals(this.Telefono, c.Telefono))
        return false;
      if(!String.Equals(this.Opcional1, c.Opcional1))
        return false;
      if(!String.Equals(this.Opcional2, c.Opcional2))
        return false;
      if(this.Estatus != c.Estatus)
        return false;
      if(this.Contratos != null && c.Contratos != null){
        if(this.Contratos.ToList().Count() > 0 && c.Contratos.ToList().Count() > 0){
          if(this.Contratos.ToList().Count() != c.Contratos.ToList().Count())
            return false;
          int count = 0;
          do{
            if(!((this.Contratos.ToList())[count].Equals(c.Contratos.ToList()[count])))
              return false;
            count++;
          }while(this.Contratos.ToList().Count() > count);
        }
      }
      if(this.Ubicaciones != null && c.Ubicaciones != null){
        if(this.Ubicaciones.ToList().Count() > 0 && c.Ubicaciones.ToList().Count() > 0){
          if(this.Ubicaciones.ToList().Count() != c.Ubicaciones.ToList().Count())
            return false;
          int count = 0;
          do{
            if(!(this.Ubicaciones.ToList()[count].Equals(c.Ubicaciones.ToList()[count])))
              return false;
            count++;
          }while(this.Ubicaciones.ToList().Count() > count);
        }
      }
      return true;
    }
  }
}
