using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.DTO
{
    public class ConfiguracionServicioDTO
    {
        public int Id { get; set; }
        public int Cliente { get; set; }
        public int Contrato { get; set; }
        public int Ubicacion { get; set; }
        public bool Estatus { get; set; }
        public string dtServicios { get; set; }

        public ConfiguracionServicioDTO(){}
    }
}
