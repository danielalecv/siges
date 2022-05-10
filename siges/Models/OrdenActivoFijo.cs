using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Models {
  public class OrdenActivoFijo {
    public int Id { get; set; }
    public ActivoFijo ActivoFijo { get; set; }
    public bool Estatus { get; set; }
    public OrdenServicio OrdenServicio {get; set;}

    public OrdenActivoFijo() {
      this.ActivoFijo = new ActivoFijo();
      this.OrdenServicio = new OrdenServicio();
    }

    public OrdenActivoFijo Copia(){
      OrdenActivoFijo copia = new OrdenActivoFijo();
      copia.Id = this.Id;
      copia.ActivoFijo = this.ActivoFijo.Copia();
      copia.Estatus = this.Estatus;
      return copia;
    }

    public override bool Equals(Object obj){
      if(obj == null)
        return false;
      OrdenActivoFijo o = obj as OrdenActivoFijo;
      if((Object)o == null)
        return false;
      if(this.Id != o.Id)
        return false;
      if(!this.ActivoFijo.Equals(o.ActivoFijo))
        return false;
      if(this.Estatus != o.Estatus)
        return false;
      return true;
    }
  }
}
