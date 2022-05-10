using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Models {
  public class ClienteUbicacion {
    public int Id { get; set; }
    public Cliente Cliente { get; set; }
    public Ubicacion Ubicacion { get; set; }
    public bool Estatus { get; set; }
    public DateTime FechaAdministrativa {get; set;}

    public ClienteUbicacion() {
      this.Cliente = new Cliente();
      this.Ubicacion = new Ubicacion();
    }
  }
}
