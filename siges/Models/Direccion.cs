using System;

namespace siges.Models {
  public class Direccion {
    public int Id { get; set; }
    public string calle { get; set; }
    public int numero { get; set; }
    public string colonia { get; set; }
    public int cp { get; set; }
    public string municipio { get; set; }
    public string entidadFederativa { get; set; }
    public bool estatus { get; set; }
  }
}
