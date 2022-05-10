using System;

namespace siges.Models {
  public class BitacoraEstatus {
    public int Id { get; set; }
    public OrdenServicio Os { get; set; }
    public string Folio { get; set; }
    public Persona QuienCambia { get; set; }
    public string Email { get; set; }
    public string De { get; set; }
    public string A { get; set; }
    public DateTime FechaAdministrativa { get; set; }
    public string Description { get; set; }
  }
}
