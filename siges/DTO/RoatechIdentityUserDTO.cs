using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace siges.DTO {
  public class RoatechIdentityUserDTO {
    public string Nombre { get; set; }
    public string Paterno { get; set; }
    public string Materno { get; set; }
    public string RFC { get; set; }
    public string CURP { get; set; }
    public string Email { get; set; }
    public string Telefono { get; set; }
    public string TelefonoContacto { get; set; }
  }
}
