using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Models {
  public class OrdenPersona : IComparable<OrdenPersona>{
    public int Id { get; set; }
    public Persona Persona { get; set; }
    public OrdenServicio OrdenServicio {get; set;}
    public bool Estatus { get; set; }

    public OrdenPersona() {
      this.Persona = new Persona();
      this.OrdenServicio = new OrdenServicio();
    }

    public int CompareTo(OrdenPersona op){
      if(op == null)
        return 1;
      else
       return this.OrdenServicio.Id.CompareTo(op.OrdenServicio.Id);
    }
  }
}
