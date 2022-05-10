using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace siges.DTO {
  public class PersonaDTO {
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Paterno { get; set; }
    public string Materno { get; set; }
    public string RFC { get; set; }
    public string CURP { get; set; }
    public string Email { get; set; }
    public string ClaveEmpleado { get; set; }
    public string Telefono { get; set; }
    public string TelefonoContacto { get; set; }
    public bool Estatus { get; set; }
    public string Puesto { get; set; }
    public string Categoria { get; set; }
    public string Direccion { get; set; }
    public string EntidadFederativa { get; set; }
    public string Municipio { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    [DataType(DataType.Currency)]
    public int Sueldo { get; set; }
    public string Opcional1 { get; set; }
    public string Opcional2 { get; set; }
    public string Adscripcion {get; set; }
    public string FaceApiId {get;set;}
    public int FaceApiCount {get;set;}
  }
}
