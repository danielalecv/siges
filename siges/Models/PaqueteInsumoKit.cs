using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Models {
  public class PaqueteInsumoKit {
    public int Id { get; set; }
    public Paquete Paquete { get; set; }
    public Insumo Insumo { get; set; }
    public Kit Kit { get; set; }
    public int CantidadKit { get; set; }
    public int CantidadInsumo { get; set; }
    public bool Estatus { get; set; }
    public DateTime FechaAdministrativa {get; set;}

    public PaqueteInsumoKit(){
      this.Paquete = new Paquete();
      this.Insumo = new Insumo();
      this.Kit = new Kit();
    }
  }
}
