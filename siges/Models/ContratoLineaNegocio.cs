using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Models {
  public class ContratoLineaNegocio {
    public int Id { get; set; }
    public Contrato Contrato { get; set; }
    public LineaNegocio LineaNegocio { get; set; }
    public bool Estatus { get; set; }
    public DateTime FechaAdministrativa {get; set;}

    public ContratoLineaNegocio() {
      this.Contrato = new Contrato();
      this.LineaNegocio = new LineaNegocio();
    }
  }
}
