using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Models {
  public class LineaNegocioServicio {
    public int Id { get; set; }
    public LineaNegocio LineaNegocio { get; set; }
    public Servicio Servicio { get; set; }
    public bool Estatus { get; set; }
    public DateTime FechaAdministrativa {get; set;}

    public LineaNegocioServicio() {
      this.LineaNegocio = new LineaNegocio();
      this.Servicio = new Servicio();
    }
  }
}
