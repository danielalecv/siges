using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Models
{
  public class Insumo
  {
    public int Id { get; set; }
    [DisplayName("Descripción")]
    public string Descripcion { get; set; }
    public string Clave { get; set; }
    public string Marca { get; set; }
    public string Tipo { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    [DataType(DataType.Currency)]
    public decimal Precio { get; set; }
    public bool Estatus { get; set; }
    [DisplayName("Mínimo")]
    public string Opcional1 { get; set; }
    [DisplayName("Máximo")]
    public string Opcional2 { get; set; }

    public Insumo Copia(){
      Insumo copia = new Insumo();
      copia.Id = this.Id;
      copia.Descripcion = this.Descripcion;
      copia.Clave = this.Clave;
      copia.Marca = this.Marca;
      copia.Tipo = this.Tipo;
      copia.Precio = this.Precio;
      copia.Estatus = this.Estatus;
      copia.Opcional1 = this.Opcional1;
      copia.Opcional2 = this.Opcional2;
      return copia;
    }

    public override bool Equals(Object obj){
      if(obj == null || GetType() != obj.GetType())
        return false;
      Insumo i = obj as Insumo;
      if((Object)i == null)
        return false;
      if(this.Id != i.Id)
        return false;
      if(!(String.Equals(this.Descripcion, i.Descripcion)))
        return false;
      if(!(String.Equals(this.Clave, i.Clave)))
        return false;
      if(!(String.Equals(this.Marca, i.Marca)))
        return false;
      if(!(String.Equals(this.Tipo, i.Tipo)))
        return false;
      if(this.Precio != i.Precio)
        return false;
      if(this.Estatus != i.Estatus)
        return false;
      if(!(String.Equals(this.Opcional1, i.Opcional1)))
        return false;
      if(!(String.Equals(this.Opcional2, i.Opcional2)))
        return false;
      return true;
    }
  }
}
