using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Models {
  public class KitInsumo {
    public int Id { get; set; }
    public Insumo Insumo { get; set; }
    public Kit Kit { get; set; }
    public int Cantidad { get; set; }
    public bool Estatus { get; set; }
    public DateTime FechaAdministrativa {get; set;}

    public KitInsumo(){
      this.Insumo = new Insumo();
      this.Kit = new Kit();
    }
  }
}
