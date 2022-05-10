using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Models {
  public class Persona : IComparable<Persona>{
    public int Id { get; set; }

    [DisplayName("Nombre(s)")]
    [StringLength(30, ErrorMessage = "Longitud máxima requerida, {1} caracteres.")]
    public string Nombre { get; set; }

    [DisplayName("Apellido Paterno")]
    [StringLength(30, ErrorMessage = "Longitud máxima requerida, {1} caracteres.")]
    public string Paterno { get; set; }

    [DisplayName("Apellido Materno")]
    [StringLength(30, ErrorMessage = "Longitud máxima requerida, {1} caracteres.")]
    public string Materno { get; set; }

    [MinLength(13, ErrorMessage = "Mínimo {1} caracteres.")]
    [StringLength(13, ErrorMessage = "Longitud requerida, {1} caracteres.")]
    [MaxLength(13, ErrorMessage = "Máximo {1} caracteres.")]
    [RegularExpression(@"^[A-Z]{4}\d{6}\w{3}$", ErrorMessage = "4 consonantes mayúsculas, 6 dígitos y 3 caracteres alfanuméricos.")] //[RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
    public string RFC { get; set; }

    [MinLength(18, ErrorMessage = "El campo CURP debe tener una longitud mínima de '{1}'.")]
    [StringLength(18)]
    [MaxLength(18)]
    [RegularExpression(@"^[A-Z]{4}\d{6}[MH][A-Z]{5}\w{2}$", ErrorMessage = "4 consonantes, 6 dígitos, 6 consonantes y 2 caracteres alfanuméricos.")] //[RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
    public string CURP { get; set; }

    public string Email { get; set; }

    public Direccion Dir { get; set; }

    [DisplayName("Clave de Empleado")]
    public string ClaveEmpleado { get; set; }

    [MinLength(10)]
    [StringLength(10)]
    [MaxLength(10)]
    [DisplayName("Teléfono")]
    public string Telefono { get; set; }

    [MinLength(10)]
    [StringLength(10)]
    [MaxLength(10)]
    [DisplayName("Celular")]
    public string TelefonoContacto { get; set; }
    public bool Estatus { get; set; }

    [StringLength(30, ErrorMessage = "Longitud máxima requerida, {1} caracteres.")]
    [DisplayName("Categoría")]
    public string Categoria { get; set; }

    [StringLength(50, ErrorMessage = "Longitud máxima requerida, {1} caracteres.")]
    [DisplayName("Puesto")]
    public string Puesto { get; set; }

    [StringLength(150, ErrorMessage = "Longitud máxima requerida, {1} caracteres.")]
    [DisplayName("Dirección")]
    public string Direccion { get; set; }

    [StringLength(50, ErrorMessage = "Longitud máxima requerida, {1} caracteres.")]
    [DisplayName("Entidad Federativa")]
    public string EntidadFederativa { get; set; }

    [StringLength(50, ErrorMessage = "Longitud máxima requerida, {1} caracteres.")]
    [DisplayName("Municipio/Alcaldía")]
    public string Municipio { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    [DataType(DataType.Currency)]
    public int Sueldo { get; set; }

    [StringLength(200, ErrorMessage = "Longitud máxima requerida, {1} caracteres.")]
    [DisplayName("Campo opcional 1")]
    public string Opcional1 { get; set; }

    [StringLength(200, ErrorMessage = "Longitud máxima requerida, {1} caracteres.")]
    [DisplayName("Campo opcional 2")]
    public string Opcional2 { get; set; }

    [StringLength(30, ErrorMessage = "Longitud máxima requerida, {1} caracteres.")]
    [DisplayName("Adscripción")]
    public string Adscripcion {get; set; }

    [DisplayName("Fotografía")]
    public byte[] Fotografia { get; set; }
    [ScaffoldColumn(false)]
    public int UsuarioId { get; set; }
    [ScaffoldColumn(false)]
    public int PerfilId { get; set; }
    public string FaceApiId {get; set;}
    public int FaceApiCount {get;set;}

    public int CompareTo(Persona p){
      if(p == null)
        return 1;
      else
        return (this.Nombre+this.Paterno+this.Materno).CompareTo(p.Nombre+p.Paterno+p.Materno);
    }
  }
}
