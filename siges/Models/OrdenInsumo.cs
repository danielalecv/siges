using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Models
{
  public class OrdenInsumo
  {
    public int Id { get; set; }
    public Insumo Insumo { get; set; }
    public Lote LoteType {get; set;}
    public string Lote {get; set;}
    public string Caducidad {get; set;}
    public int Cantidad { get; set; }
    public bool Estatus { get; set; }
    public OrdenServicio OrdenServicio {get; set;}

    public OrdenInsumo(){
      this.Insumo = new Insumo();
      this.OrdenServicio = new OrdenServicio();
    }

    public OrdenInsumo Copia(){
      OrdenInsumo copia = new OrdenInsumo();
      copia.Id = this.Id;
      copia.Insumo = this.Insumo.Copia();
      copia.Cantidad = this.Cantidad;
      copia.Estatus = this.Estatus;
      return copia;
    }

    public override bool Equals(Object obj){
      if(obj == null || GetType() != obj.GetType())
        return false;
      OrdenInsumo o = obj as OrdenInsumo;
      if((Object)o == null)
        return false;
      if(this.Id != o.Id)
        return false;
      if(!this.Insumo.Equals(o.Insumo))
        return false;
      if(this.Cantidad != o.Cantidad)
        return false;
      if(this.Estatus != o.Estatus)
        return false;
      return true;
    }
  }
}
