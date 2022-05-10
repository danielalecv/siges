using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using siges.Areas.Identity.Data;

namespace siges.Models {
  public class ClienteIdentity {
    public int Id { get; set; }
    public Cliente Cliente { get; set; }
    public RoatechIdentityUser CuentaUsuario { get; set; }
    public bool Estatus { get; set; }
    public DateTime FechaAdministrativa {get; set;}

    public ClienteIdentity() {
      this.Cliente = new Cliente();
      this.CuentaUsuario = new RoatechIdentityUser();
    }
  }
}
