using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Models
{
  public class ActivoFijo
  {
    public int Id { get; set; }
    [DisplayName("Producto")]
    public string Descripcion { get; set; }
    [DisplayName("Serie o Producción")]
    public string Clave { get; set; }
    public string Marca { get; set; }
    public string Tipo { get; set; }
    [DisplayName("Número de serie")]
    public string NumeroSerie {get; set;}
    [DisplayName("Fecha Factura")]
    public DateTime FechaFactura {get; set;}
    public DateTime FechaAlta {get; set;}
    [Column(TypeName = "decimal(18, 2)")]
    [DataType(DataType.Currency)]
    public decimal Precio { get; set; }
    public bool Estatus { get; set; }
    [DisplayName("Folio Factura")]
    public string Opcional1 { get; set; }
    [DisplayName("No Producto")]
    public string Opcional2 { get; set; }

    public ActivoFijo Copia(){
      ActivoFijo copia = new ActivoFijo();
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
      if (obj == null)
        return false;
      ActivoFijo a = obj as ActivoFijo;
      if((ActivoFijo) a == null)
        return false;
      if(this.Id != a.Id)
        return false;
      if(!(String.Equals(this.Descripcion, a.Descripcion)))
        return false;
      if(!(String.Equals(this.Clave, a.Clave)))
        return false;
      if(!(String.Equals(this.Marca, a.Marca)))
        return false;
      if(!(String.Equals(this.Tipo, a.Tipo)))
        return false;
      if(this.Precio != a.Precio)
        return false;
      if(this.Estatus != a.Estatus)
        return false;
      if(!(String.Equals(this.Opcional1, a.Opcional1)))
        return false;
      if(!(String.Equals(this.Opcional2, a.Opcional2)))
        return false;
      return true;
    }
  }
}
