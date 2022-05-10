using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.DTO
{
    public class OrdenServicioDTO
    {
        public int Id { get; set; }
        //public string Folio { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public int Cliente { get; set; }
        public int Contrato { get; set; }
        public int Ubicacion { get; set; }
        public int LineaNegocio { get; set; }
        public int Servicio { get; set; }
        public string Tipo { get; set; }
        public string EstatusServicio { get; set; }
        public string Observaciones { get; set; }
        public string ContactoNombre { get; set; }
        public string ContactoAP { get; set; }
        public string ContactoAM { get; set; }
        public string ContactoEmail { get; set; }
        public string ContactoTelefono { get; set; }
        public string NombreCompletoCC1 {get; set;}
        public string EmailCC1 {get; set;}
        public string NombreCompletoCC2 {get; set;}
        public string EmailCC2 {get; set;}
        public string Opcional1 { get; set; }
        public string Opcional2 { get; set; }
        public string Opcional3 { get; set; }
        public string Opcional4 { get; set; }
        public bool Estatus { get; set; }
        public string dtPersonas { get; set; }
        public string dtActivos { get; set; }
        public string dtInsumos { get; set; }

        public OrdenServicioDTO() {}
    }
}
