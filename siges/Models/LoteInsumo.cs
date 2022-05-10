using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Models {
  public class LoteInsumo {
    public int Id { get; set; }
    public Insumo Insumo { get; set; }
    public Lote Lote { get; set; }
    public int Cantidad { get; set; }
    public bool Estatus { get; set; }
    public DateTime FechaAdministrativa {get; set;}

    public LoteInsumo(){
      this.Insumo = new Insumo();
      this.Lote = new Lote();
    }
  }
}
